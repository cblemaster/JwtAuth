using FluentValidation;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.Core.Validation;

public class CreateProfileDTOValidator : AbstractValidator<CreateProfileDTO>
{
    public CreateProfileDTOValidator()
    {
        RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required.")
            .MaximumLength(255).WithMessage("First name must be 255 characters or fewer.");
        RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(255).WithMessage("Last name must be 255 characters or fewer.");
        RuleFor(p => p.Email).NotEmpty().WithMessage("Email is required.")
            .MaximumLength(255).WithMessage("Email must be 255 characters or fewer.");
        // TODO: Rule for unique email
        RuleFor(p => p.Phone).NotEmpty().WithMessage("Phone is required.")
            .MaximumLength(10).WithMessage("Phone must be 10 characters or fewer.");
        // TODO: Rule for unique phone
    }
}
