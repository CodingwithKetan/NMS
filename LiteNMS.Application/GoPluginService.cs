using System.Diagnostics;
using System.Text.Json;
using Entities.Models;
using LIteNMS.Application.Contracts;
using LiteNMS.DTOS;
using LiteNMS.Infrastructure;
using LiteNMS.Utils.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LiteNMS.Services;

public class GoPluginService : IGoPluginService
{
    private readonly ILogger<GoPluginService> _logger;
    private readonly ICredentialProfileRepository _credentialProfileRepository;
    private readonly string _pluginPath;
    
    public GoPluginService(ILogger<GoPluginService> logger, ICredentialProfileRepository credentialProfileRepository)
    {
        _logger = logger;
        _credentialProfileRepository = credentialProfileRepository;
        _pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", "go_discovery_plugin");
        
        if (!File.Exists(_pluginPath))
        {
            throw new FileNotFoundException($"Go plugin not found at {_pluginPath}");
        }
    }

    public async Task<IEnumerable<DeviceDiscoveryResultDto>> RunDiscovery(DiscoveryProfile request)
    {
        _logger.LogInformation($"Running discovery for {request.IPRanges.Count} IPs");
        var discoveredDevices = new List<DeviceDiscoveryResultDto>();

        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = _pluginPath,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Console.WriteLine(processStartInfo.FileName);
            
            using var process = new Process { StartInfo = processStartInfo };
            process.Start();
            
            await using var streamWriter = process.StandardInput;
            if (!streamWriter.BaseStream.CanWrite)
            {
                throw new Exception("Failed to open input stream for Go plugin");
            }

            var credentialProfiles = await _credentialProfileRepository.FindByConditionAsync(_ => request.CredentialProfileIds.Contains(_.Id));
            var deviceDiscoveryRequest = new DeviceDiscoveryRequestDto()
            {
                Discovery = new DiscoveryRequest()
                {
                    Ipranges = request.IPRanges, Credentials = credentialProfiles.Select(ToCredentialDto).ToList(),
                    Port = request.Port,
                }
            };
            
            
            var requestJson = JsonSerializer.Serialize(deviceDiscoveryRequest, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            Console.WriteLine(requestJson);
            await streamWriter.WriteAsync(requestJson);
            await streamWriter.FlushAsync();
            streamWriter.Close();

            var output = await process.StandardOutput.ReadToEndAsync();
            
            Console.WriteLine("writing output");
            Console.WriteLine(output);
            process.WaitForExit();
            
            _logger.LogInformation($"Go Plugin Output: {output}");
            
            if (!string.IsNullOrWhiteSpace(output))
            {
                try
                {
                    discoveredDevices = JsonSerializer.Deserialize<List<DeviceDiscoveryResultDto>>(output, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<DeviceDiscoveryResultDto>();
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError($"Failed to deserialize Go plugin output: {jsonEx.Message}");
                }
            }
            else
            {
                _logger.LogWarning("Go plugin returned an empty response.");
            }
            
            _logger.LogInformation($"Discovery completed. Found {discoveredDevices.Count} devices.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during discovery: {ex.Message}");
        }

        return discoveredDevices;
    }

    private static CredentialDto ToCredentialDto(CredentialProfile credentialProfile)
    {
        return new CredentialDto()
        {
            Id = credentialProfile.Id,
            UserName = credentialProfile.Username,
            Password = EncryptionHelper.Decrypt(credentialProfile.EncryptedPassword,
                "your-secure-32char-key-123456789")

        };
    }
}
