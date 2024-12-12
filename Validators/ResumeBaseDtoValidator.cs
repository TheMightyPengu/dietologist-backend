using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class ResumeBaseDtoValidator : AbstractValidator<ResumeBaseDto>
{
    public ResumeBaseDtoValidator()
    {
        RuleFor(dto => dto.FileName)
            .NotEmpty().WithMessage("FileName is required.")
            .MaximumLength(200).WithMessage("FileName cannot exceed 200 characters.");
    }
}