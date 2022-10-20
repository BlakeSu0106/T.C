using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Telligent.Core.Infrastructure.Encryption;

/// <summary>
/// Encryption Helper
/// </summary>
public static class EncryptionHelper
{
    /// <summary>
    /// Hash
    /// </summary>
    /// <param name="salt"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string Hash(string salt, string password)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, Encoding.UTF8.GetBytes(salt),
            KeyDerivationPrf.HMACSHA1, 1000, 256 / 8));
    }

    /// <summary>
    /// AES Encrypt
    /// </summary>
    /// <param name="text"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string EncryptAes(string text, string key, string iv)
    {
        var sourceBytes = Encoding.UTF8.GetBytes(text);
        var aes = Aes.Create();

        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(iv);
        var transform = aes.CreateEncryptor();

        return Convert.ToBase64String(transform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length));
    }

    /// <summary>
    /// SHA1 Encrypt
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string EncryptSha1(string text)
    {
        var sha1 = SHA1.Create();
        var sourceByte = Encoding.UTF8.GetBytes(text);

        return BitConverter.ToString(sha1.ComputeHash(sourceByte)).Replace("-", string.Empty);
    }

    /// <summary>
    /// AES Decrypt
    /// </summary>
    /// <param name="text"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string DecryptAes(string text, string key, string iv)
    {
        var encryptBytes = Convert.FromBase64String(text);
        var aes = Aes.Create();

        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(iv);
        var transform = aes.CreateDecryptor();

        return Encoding.UTF8.GetString(transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));
    }

    /// <summary>
    /// Argon2 Encrypt
    /// </summary>
    /// <param name="password">密碼</param>
    /// <param name="salt">鹽</param>
    /// <returns></returns>
    public static string EncryptArgon2(string password, byte[] salt)
    {
        var inputBytes = Encoding.UTF8.GetBytes(password);

        var argon = new Argon2id(inputBytes);
        argon.Salt = salt;
        argon.Iterations = 4; // 迭代運算次數
        argon.DegreeOfParallelism = 4; // 2 核心就設成 4
        argon.MemorySize = (int)Math.Pow(2.0, 12);

        var hash = Convert.ToBase64String(argon.GetBytes(16));

        return hash;
    }
}