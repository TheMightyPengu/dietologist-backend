using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class ProvidedServicesBaseDtoValidator : AbstractValidator<ProvidedServicesBaseDto>
{
    public ProvidedServicesBaseDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(dto => dto.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category cannot exceed 100 characters.");

        RuleFor(dto => dto.Duration)
            .GreaterThan(0).WithMessage("Duration must be greater than 0.")
            .LessThanOrEqualTo(360).WithMessage("Duration cannot exceed 360 minutes (6 hours).");

        RuleFor(dto => dto.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(dto => dto.PriceIncludingVAT)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.")
            .ScalePrecision(2, 10).WithMessage("Price can have up to 2 decimal places and 10 digits in total.");

        RuleFor(dto => dto.IntervalInDays)
            .GreaterThanOrEqualTo(0).WithMessage("IntervalInDays must be greater than or equal to 0.")
            .LessThanOrEqualTo(365).WithMessage("IntervalInDays cannot exceed 365 days.");
    }
}