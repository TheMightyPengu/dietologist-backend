using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class SocialMediaLinksBaseDtoValidator : AbstractValidator<SocialMediaLinksBaseDto>
{
    public SocialMediaLinksBaseDtoValidator()
    {
        RuleFor(dto => dto.PlatformName)
            .NotEmpty().WithMessage("PlatformName is required.")
            .MaximumLength(100).WithMessage("PlatformName cannot exceed 100 characters.");

        // RuleFor(dto => dto.Url)
        //     .NotEmpty().WithMessage("Url is required.")
        //     .Matches(@"^(https?:\/\/)").WithMessage("Url must be a valid URL.");
    }
}