using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class ImagesBaseDtoValidator : AbstractValidator<ImagesBaseDto>
{
    public ImagesBaseDtoValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

        // RuleFor(dto => dto.ImageUrl)
        //     .NotEmpty().WithMessage("ImageUrl is required.")
        //     .Matches(@"^(https?:\/\/)").WithMessage("ImageUrl must be a valid URL.");
    }
}