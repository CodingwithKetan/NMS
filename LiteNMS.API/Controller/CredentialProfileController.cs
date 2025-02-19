using System.ComponentModel.DataAnnotations;
using Entities.Models;
using LIteNMS.Application.Contracts;
using LiteNMS.DTOS;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace LiteNMS.API.Controller;

    [Route("api/credential-profiles")]
    [ApiController]
    public class CredentialProfileController : ControllerBase
    {
        private readonly ICredentialProfileService _service;
        private readonly IValidator<CredentialProfileRequest> _validator;

        public CredentialProfileController(ICredentialProfileService service, IValidator<CredentialProfileRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responses = await _service.GetAllProfilesAsync();
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _service.GetProfileByIdAsync(id);
            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CredentialProfileRequest request)
        {
            ValidationResult result = await _validator.ValidateAsync(request);
            if (!result.IsValid) return BadRequest(result.Errors);

            var createdId = await _service.AddProfileAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = createdId }, new { Id = createdId });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CredentialProfileRequest request)
        {
            ValidationResult result = await _validator.ValidateAsync(request);
            if (!result.IsValid) return BadRequest(result.Errors);
            
            await _service.UpdateProfileAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteProfileAsync(id);
            return NoContent();
        }
    }