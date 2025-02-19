using LIteNMS.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LiteNMS.API.Controller;

[Route("api/discovery")]
[ApiController]
public class DiscoveryController : ControllerBase
{
    private readonly IDiscoveryService _service;
    public DiscoveryController(IDiscoveryService service) => _service = service;
    
    
    [HttpPost("run/{profileId}")]
    public async Task<IActionResult> RunDiscovery(Guid profileId)
    {
        var deviceDiscoveryResultUiDtos = await _service.RunDiscoveryAsync(profileId);
        return Ok(deviceDiscoveryResultUiDtos);
    }
}

