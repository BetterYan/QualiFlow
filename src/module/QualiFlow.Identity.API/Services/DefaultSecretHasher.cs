using QualiFlow.Identity.API.Contracts;
using QualiFlow.Identity.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace QualiFlow.Identity.API.Services;

internal class DefaultSecretHasher : ISecretHasher
{
    public HashedSecret HashSecret(string secret)
    {
        var saltBytes = GenerateSalt();
        return HashSecret(secret, saltBytes);
    }

    public HashedSecret HashSecret(string secret, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(secret);
        var hashedPassword = HashSecret(passwordBytes, salt);
        return HashedSecret.FromBytes(hashedPassword, salt);
    }

    public bool VerifySecret(string clearTextSecret, string secret, string salt)
    {
        var hashedPassword = HashedSecret.FromString(secret, salt);
        return VerifySecret(clearTextSecret, hashedPassword);
    }

    public bool VerifySecret(string clearTextSecret, HashedSecret hashedSecret)
    {
        var password = hashedSecret.Secret;
        var salt = hashedSecret.Salt;
        var providedHashedPassword = HashSecret(clearTextSecret, salt);
        return providedHashedPassword.Secret.SequenceEqual(password);
    }

    public byte[] HashSecret(byte[] secret, byte[] salt)
    {
        using var sha256 = SHA256.Create();
        var passwordAndSalt = secret.Concat(salt).ToArray();
        return sha256.ComputeHash(passwordAndSalt);
    }

    public byte[] GenerateSalt(int saltSize = 32) => RandomNumberGenerator.GetBytes(saltSize);
}
