using Application.Extensions;
using Domain.Constants;
using FluentValidation;

namespace Application.Features.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmptyLocal();

        RuleFor(p => p.FirstName)
            .NotEmptyLocal()
            .MaximumLengthLocal(EntityConstraints.User.FirstNameMaxLength);

        RuleFor(p => p.LastName)
            .NotEmptyLocal()
            .MaximumLengthLocal(EntityConstraints.User.LastNameMaxLength);
    }
} 