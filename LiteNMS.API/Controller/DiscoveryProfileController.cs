using FluentValidation;
using LIteNMS.Application.Contracts;
using LiteNMS.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace LiteNMS.API.Controller;

// DiscoveryProfileController
[ApiController]
[Route("api/discovery-profile")]
public class DiscoveryProfileController : ControllerBase
{
    private readonly IDiscoveryProfileService _service;
    private readonly IValidator<DiscoveryProfileRequestDto> _validator;
    public DiscoveryProfileController(IDiscoveryProfileService service, IValidator<DiscoveryProfileRequestDto> validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var profile = await _service.GetByIdAsync(id);
        return profile != null ? Ok(profile) : NotFound();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] DiscoveryProfileRequestDto profileDto)
    {
        var validationResult = await _validator.ValidateAsync(profileDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var createdId = await _service.AddAsync(profileDto);
        return CreatedAtAction(nameof(GetById), new { id = createdId }, new { Id = createdId });
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DiscoveryProfileRequestDto profileDto)
    {
        var validationResult = await _validator.ValidateAsync(profileDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        await _service.UpdateAsync(id, profileDto);
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}