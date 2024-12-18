using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Queries;
using Domain.Entities;

namespace Application.Mapping;

public static class UserMapper
{
    public static User ToEntity(this CreateUserCommand command)
    {
        return new User()
        {
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Password = command.Password,
        };
    }

    public static CreateUserResponse ToCreateResponse(this User user) // this naming should be clarified for cqrs responses. Multiple command and queries can have different response types.
    {
        return new CreateUserResponse(user.Id);
    }

    public static UpdateUserResponse ToUpdateResponse(this User user)
    {
        return new UpdateUserResponse(user.Id, user.FirstName, user.LastName);
    }

    public static List<GetUsersResponse> ToGetUsersResponse(this IEnumerable<User> users)
    {
        var response = users.Select(p =>
            new GetUsersResponse(
                p.Id,
                p.Email,
                p.FirstName,
                p.LastName)
            ).ToList();

        return response;
    }
}
