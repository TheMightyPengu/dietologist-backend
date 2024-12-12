using dietologist_backend.DTO;
using FluentValidation;

namespace dietologist_backend.Validators;

public class NewsletterSubscribersBaseDtoValidator : AbstractValidator<NewsletterSubscribersBaseDto>
{
    public NewsletterSubscribersBaseDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .EmailAddress().WithMessage("A valid email address is required.");
    }
}