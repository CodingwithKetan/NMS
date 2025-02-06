using Microsoft.AspNetCore.Mvc;
using NMS.WebAPI.Dtos;
using Repository.Contract.Models;
using Service.Contract;

namespace NMS.WebAPI;

[Route("api/discovery-profile")]
[ApiController]
public class DiscoveryProfileController : ControllerBase
{
    private readonly IDiscoveryProfileService _service;

    public DiscoveryProfileController(IDiscoveryProfileService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateDiscoveryProfileDto dto)
    {
        var profile = new DiscoveryProfile { Name = dto.Name, Subnet = dto.Subnet, ScanType = dto.ScanType, Timeout = dto.Timeout, Retries = dto.Retries };
        await _service.AddAsync(profile);
        return CreatedAtAction(nameof(GetById), new { id = profile.Id }, profile);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var profile = await _service.GetByIdAsync(id);
        return profile == null ? NotFound() : Ok(profile);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDiscoveryProfileDto dto)
    {
        var profile = await _service.GetByIdAsync(id);
        if (profile == null) return NotFound();

        profile.Name = dto.Name;
        profile.Subnet = dto.Subnet;
        profile.ScanType = dto.ScanType;
        profile.Timeout = dto.Timeout;
        profile.Retries = dto.Retries;

        await _service.UpdateAsync(profile);
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
