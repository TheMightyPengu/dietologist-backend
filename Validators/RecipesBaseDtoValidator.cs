using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class RecipesBaseDtoValidator : AbstractValidator<RecipesBaseDto>
{
    public RecipesBaseDtoValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(dto => dto.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}