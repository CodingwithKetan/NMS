using Microsoft.AspNetCore.Mvc;
using Repository.Contract.Models;
using Service.Contract;

namespace NMS.WebAPI.Controllers;

[Route("api/credential-profiles")]
[ApiController]
public class CredentialProfileController : ControllerBase
{
    private readonly ICredentialProfileService _service;

    public CredentialProfileController(ICredentialProfileService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var profiles = await _service.GetAllAsync();
        return Ok(profiles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var profile = await _service.GetByIdAsync(id);
        if (profile == null) return NotFound();
        return Ok(profile);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CredentialProfile profile)
    {
        await _service.AddAsync(profile);
        return CreatedAtAction(nameof(GetById), new { id = profile.Id }, profile);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CredentialProfile profile)
    {
        if (id != profile.Id) return BadRequest();
        await _service.UpdateAsync(profile);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
