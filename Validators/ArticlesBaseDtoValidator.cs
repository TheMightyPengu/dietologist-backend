using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class ArticlesBaseDtoValidator : AbstractValidator<ArticlesBaseDto>
{
    public ArticlesBaseDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(dto => dto.Subtitle)
            .MaximumLength(200).WithMessage("Subtitle cannot exceed 200 characters.");

        RuleFor(dto => dto.Heading)
            .MaximumLength(300).WithMessage("Heading cannot exceed 300 characters.");

        RuleFor(dto => dto.Content)
            .NotEmpty().WithMessage("Content is required.");

        // RuleFor(dto => dto.ImageUrl)
        //     .NotEmpty().WithMessage("ImageUrl is required.")
        //     .Matches(@"^(https?:\/\/)").WithMessage("ImageUrl must be a valid URL.");
    }
}