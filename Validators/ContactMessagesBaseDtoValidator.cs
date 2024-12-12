using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class ContactMessagesBaseDtoValidator : AbstractValidator<ContactMessagesBaseDto>
{
    public ContactMessagesBaseDtoValidator()
    {
        RuleFor(dto => dto.SenderName)
            .NotEmpty().WithMessage("SenderName is required.")
            .MaximumLength(100).WithMessage("SenderName cannot exceed 100 characters.");

        RuleFor(dto => dto.SenderEmail)
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(dto => dto.Message)
            .NotEmpty().WithMessage("Message is required.")
            .MaximumLength(1000).WithMessage("Message cannot exceed 1000 characters.");
    }
}