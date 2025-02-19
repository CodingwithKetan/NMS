using LIteNMS.Application.Contracts;
using LiteNMS.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace LiteNMS.API.Controller;

[ApiController]
[Route("api/device-provision")]
public class DeviceProvisionController : ControllerBase
{
    private readonly IDeviceProvisionService _deviceProvisionService;

    public DeviceProvisionController(IDeviceProvisionService deviceProvisionService)
    {
        _deviceProvisionService = deviceProvisionService;
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartProvisioning([FromBody] DeviceProvisionRequestDto request)
    {
        if (request.DiscoveryProfileId == Guid.Empty || request.PollTime <= 0)
            return BadRequest("Invalid input parameters");

        var result = await _deviceProvisionService.ProvisionDevices(request);
        return Ok(result);
    }
}