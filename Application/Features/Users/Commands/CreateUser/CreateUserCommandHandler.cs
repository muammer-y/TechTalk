using Application.Abstractions.Messaging;
using Application.Helpers;
using Application.Mapping;
using Infrastructure.Data;
using Shared.Result;

namespace Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Email, string Password, string FirstName, string LastName) : ICommand<CreateUserResponse>;

public record CreateUserResponse(int Id);

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUnitOfWork uow, IPasswordHasher passwordHasher)
    {
        _uow = uow;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var isUserExists = await _uow.UserRepository.FirstOrDefaultAsync(p => p.Email == command.Email);

        if (isUserExists is not null)
        {
            return UserErrors.UserWithEmailExists;
        }

        var hashedPassword = _passwordHasher.HashPassword(command.Password);

        var entity = command.ToEntity();
        entity.Password = hashedPassword;

        await _uow.UserRepository.AddAsync(entity);
        await _uow.SaveChangesAsync();

        var response = entity.ToCreateResponse();

        return response;
    }
}

