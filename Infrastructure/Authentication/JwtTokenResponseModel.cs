namespace Infrastructure.Authentication;

public class JwtTokenResponseModel
{
    public required string AccessToken { get; set; }
    public DateTime AccessTokenExpiresAt { get; set; }
}
