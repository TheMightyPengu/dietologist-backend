using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class EbooksBaseDtoValidator : AbstractValidator<EbooksBaseDto>
{
    public EbooksBaseDtoValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(dto => dto.Author)
            .NotEmpty().WithMessage("Author is required.")
            .MaximumLength(100).WithMessage("Author cannot exceed 100 characters.");

        RuleFor(dto => dto.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.")
            .ScalePrecision(2, 10).WithMessage("Price can have up to 2 decimal places.");

        // RuleFor(dto => dto.FileUrl)
        //     .NotEmpty().WithMessage("FileUrl is required.")
        //     .Matches(@"^(https?:\/\/)").WithMessage("FileUrl must be a valid URL.");
    }
}