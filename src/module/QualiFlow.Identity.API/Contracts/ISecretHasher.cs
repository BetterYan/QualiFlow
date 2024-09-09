using QualiFlow.Identity.API.Models;

namespace QualiFlow.Identity.API.Contracts;

public interface ISecretHasher
{
    byte[] GenerateSalt(int saltSize = 32);
    HashedSecret HashSecret(string secret);
    byte[] HashSecret(byte[] secret, byte[] salt);
    HashedSecret HashSecret(string secret, byte[] salt);
    bool VerifySecret(string clearTextSecret, HashedSecret hashedSecret);
    bool VerifySecret(string clearTextSecret, string secret, string salt);
}
