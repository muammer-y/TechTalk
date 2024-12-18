using NanoidDotNet;
using System.Security.Cryptography;

namespace Application.Helpers;

public static class CodeGenerator
{
    private const int DefaultOtpCodeLength_LowerBound = 100_000;
    private const int DefaultOtpCodeLength_UpperBound = 999_999;

    public static string GenerateOtpCode(int lowerBound = DefaultOtpCodeLength_LowerBound, int upperBound = DefaultOtpCodeLength_UpperBound)
    {
        var randomNumber = RandomNumberGenerator.GetInt32(lowerBound, upperBound);
        return randomNumber.ToString();
    }

    public static string RandomCode()
    {
        var random = new Random();
        return random.Next(DefaultOtpCodeLength_LowerBound, DefaultOtpCodeLength_UpperBound).ToString();
    }
}


/// <summary>
/// <see cref="https://github.com/codeyu/nanoid-net"/>
/// <para></para>
/// <see cref="https://github.com/ai/nanoid"/>
/// </summary>
public static class AlphanumericGenerator
{
    private const string _defaultAlphabet = Nanoid.Alphabets.LowercaseLettersAndDigits;
    private const int _defaultCodeLength = 15;

    public static async Task<string> GenerateCode(string alphabet = _defaultAlphabet ,int length = _defaultCodeLength)
    {
        return await Nanoid.GenerateAsync(alphabet, length);
    }
}

public static class RandomGenerator_Old
{
    private static readonly RNGCryptoServiceProvider rngProvider = new RNGCryptoServiceProvider();

    public static int GenerateSixDigitRandomNumber()
    {
        byte[] randomBytes = new byte[4]; // 4 byte as Int32 size

        rngProvider.GetBytes(randomBytes);

        int randomNumber = BitConverter.ToInt32(randomBytes, 0) & 0x7FFFFFFF; // positive conversion by bitwise and operation

        /*
         *  0x7FFFFFFF = 01111111 11111111 11111111 11111111 (32 bits)
         *  
         *  randomBytes = [0xA5, 0x3F, 0xFF, 0x8B]  // random 32-bit signed integer
         *  0xA53FFF8B = 1010 0101 0011 1111 1111 1111 1000 1011
         *  
         *  1010 0101 0011 1111 1111 1111 1000 1011  // Original number
         *  &
         *  0111 1111 1111 1111 1111 1111 1111 1111  // Mask (0x7FFFFFFF)
         *  -----------------------------------------
         *  0010 0101 0011 1111 1111 1111 1000 1011  // Result (positive number)
         */

        return randomNumber % 900_000 + 100_000; // modulus to fit between desired ranges.

    }
}