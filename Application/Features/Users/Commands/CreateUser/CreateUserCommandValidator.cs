using Application.Extensions;
using Domain.Constants;
using FluentValidation;
using Shared.Constants;

namespace Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmptyLocal()
            .EmailAddress().WithMessage(Messages.Validation.EmailAddressInvalid);

        RuleFor(p => p.Password)
            .NotEmptyLocal()
            .LengthLocal(EntityConstraints.User.PasswordMinLength, EntityConstraints.User.PasswordMaxLength, Messages.Validation.PasswordLengthInvalid)
            .Must(ValidateComplexity).WithMessage(Messages.Validation.PasswordComplexity);

        RuleFor(p => p.FirstName)
            .NotEmptyLocal()
            .MaximumLengthLocal(EntityConstraints.User.FirstNameMaxLength);
        
        RuleFor(p => p.LastName)
            .NotEmptyLocal()
            .MaximumLengthLocal(EntityConstraints.User.LastNameMaxLength);
    }

    private static bool ValidateComplexity(string password)
    {
        return password is not null &&
            password.Any(char.IsUpper) &&
            password.Any(char.IsLower) &&
            password.Any(char.IsDigit);
    }
}
