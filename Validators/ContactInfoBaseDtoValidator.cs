using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class ContactInfoBaseDtoValidator : AbstractValidator<ContactInfoBaseDto>
{
    public ContactInfoBaseDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(dto => dto.Telephone)
            .Matches(@"^\+?\d{10,15}$").WithMessage("A valid phone number is required.");

        RuleFor(dto => dto.Location)
            .MaximumLength(300).WithMessage("Location cannot exceed 300 characters.");
    }
}