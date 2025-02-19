using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;
using LiteNMS.DTOS;

public class DiscoveryProfileDtoValidator : AbstractValidator<DiscoveryProfileRequestDto>
{
    public DiscoveryProfileDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        /*RuleForEach(x => x.IpRanges)
            .Must(IsValidIPRange)
            .WithMessage("One or more IP ranges are invalid.");*/

        RuleForEach(x => x.CredentialProfileIds)
            .NotEmpty().WithMessage("CredentialProfileId is required.");

        RuleFor(x => x.Port)
            .InclusiveBetween(1, 65535).WithMessage("Port must be between 1 and 65535.");
    }

    private bool IsValidIPRange(string ipRange)
    {
        var rangePattern = new Regex(@"^(\d+\.\d+\.\d+\.)(\d+)-(\d+)$");
        var match = rangePattern.Match(ipRange);
        
        if (match.Success)
        {
            int start, end;
            if (int.TryParse(match.Groups[2].Value, out start) && int.TryParse(match.Groups[3].Value, out end))
            {
                return start <= end && start >= 0 && end <= 255;
            }
        }
        return false;
    }
}
