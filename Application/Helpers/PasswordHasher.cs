using System.Security.Cryptography;

namespace Application.Helpers;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}

/// <summary>
/// <see cref="https://github.com/dotnet/aspnetcore/blob/main/src/Identity/Extensions.Core/src/PasswordHasher.cs" />
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int _saltSize = 16;
    private const int _hashSize = 32;
    private const int _iterations = 100_000;
    private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;
    private const string _delimiter = ";";

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _algorithm, _hashSize);

        return $"{Convert.ToBase64String(hash)};{Convert.ToBase64String(salt)}";
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        var parts = hashedPassword.Split(_delimiter);

        var hash = Convert.FromBase64String(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);

        var inputHash = Rfc2898DeriveBytes.Pbkdf2(providedPassword, salt, _iterations, _algorithm, _hashSize);

        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
}
