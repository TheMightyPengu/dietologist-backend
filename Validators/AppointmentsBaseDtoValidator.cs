using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class AppointmentsBaseDtoValidator : AbstractValidator<AppointmentsBaseDto>
{
    public AppointmentsBaseDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(dto => dto.ServiceId)
            .GreaterThan(0).WithMessage("ServiceId must be greater than 0.");

        RuleFor(dto => dto.ProvidedService)
            .NotNull().WithMessage("ProvidedService is required.");

        RuleFor(dto => dto.AppointmentDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("AppointmentDate must be in the future.");

        RuleFor(dto => dto.CustomerName)
            .NotEmpty().WithMessage("CustomerName is required.")
            .MaximumLength(100).WithMessage("CustomerName cannot exceed 100 characters.");

        RuleFor(dto => dto.CustomerEmail)
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(dto => dto.CustomerPhone)
            .Matches(@"^\+?\d{10,15}$").WithMessage("A valid phone number is required.");
    }
}