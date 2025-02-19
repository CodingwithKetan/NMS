using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using LiteNMS.DTOS;
using LiteNMS.Infrastructure;

namespace LiteNMS.Validators
{
    public class CredentialProfileValidator : AbstractValidator<CredentialProfileRequest>
    {
        private readonly ICredentialProfileRepository _repository;

        public CredentialProfileValidator(ICredentialProfileRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.ProfileName)
                .NotEmpty().WithMessage("Profile Name is required")
                .MaximumLength(100).WithMessage("Profile Name cannot exceed 100 characters")
                .MustAsync(BeUniqueProfileName).WithMessage("Profile Name already exists");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(100).WithMessage("Username cannot exceed 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        }

        private async Task<bool> BeUniqueProfileName(string profileName, CancellationToken cancellationToken)
        {
            return !(await _repository.ProfileNameExistsAsync(profileName));
        }
    }
}