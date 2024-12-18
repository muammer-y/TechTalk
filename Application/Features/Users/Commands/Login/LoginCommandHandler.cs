using Application.Abstractions.Messaging;
using Application.Helpers;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Shared.Result;

namespace Application.Features.Users.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;

public class LoginResponse : JwtTokenResponseModel
{
    public static LoginResponse FromToken(JwtTokenResponseModel tokenModel) => 
        new() { AccessToken = tokenModel.AccessToken, AccessTokenExpiresAt = tokenModel.AccessTokenExpiresAt };
}

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(p => p.Email.Equals(request.Email));

        if (user is null || !_passwordHasher.VerifyHashedPassword(user.Password, request.Password))
        {
            return UserErrors.User_WrongCredentials;
        }

        var tokenModel = _jwtTokenService.GenerateToken(user);
        return LoginResponse.FromToken(tokenModel);
    }
}
