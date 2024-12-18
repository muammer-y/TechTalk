using Shared.Result;

namespace Application.Features.Users;

public static class UserErrors
{
    public static readonly Error UserWithEmailExists = new("Users.EmailExists", "A user with the provided email already exists.");

    public static readonly Error User_WrongCredentials = new("Users.WrongCredentials", "User with the provided credentials not found");

    public static readonly Error User_NotFound = new("Users.NotFound", "User with the given info not found");
}