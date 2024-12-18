using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers;

public static class EncryptionHelper
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("ThisIsASecretKey12345678901234");  // 256-bit key (32 bytes)
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("ThisIsAnIV123456");  // 128-bit IV (16 bytes)

    // AES Key and IV Lengths: AES requires fixed sizes for both the key and the IV.
    // AES - 128 requires a 16 - byte key and 16 - byte IV.
    // AES - 192 requires a 24 - byte key and 16 - byte IV.
    // AES - 256 requires a 32 - byte key and 16 - byte IV.
    // Key and iv input values should be checked. If it's converted from a string each char being ascii helps to count as 1 byte

    public static string Encrypt(string plainText)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = Key;
        aesAlg.IV = IV;
        aesAlg.KeySize = 256;
        aesAlg.BlockSize = 128;

        using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();

        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        var ivAndCipherText = aesAlg.IV.Concat(msEncrypt.ToArray()).ToArray();
        return Convert.ToBase64String(ivAndCipherText);
    }

    public static string Decrypt(string cipherText)
    {
        var ivAndCipherText = Convert.FromBase64String(cipherText);
        var iv = new byte[16];
        Array.Copy(ivAndCipherText, iv, iv.Length);

        var cipherBytes = new byte[ivAndCipherText.Length - iv.Length];
        Array.Copy(ivAndCipherText, iv.Length, cipherBytes, 0, cipherBytes.Length);

        using var aesAlg = Aes.Create();
        aesAlg.Key = Key;
        aesAlg.IV = iv;
        aesAlg.KeySize = 256;
        aesAlg.BlockSize = 128;

        using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        using var msDecrypt = new MemoryStream(cipherBytes);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}
