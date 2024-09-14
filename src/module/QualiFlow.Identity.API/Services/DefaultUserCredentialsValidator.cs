using Microsoft.EntityFrameworkCore;
using QualiFlow.EntityFrameworkCore.SqlServer;
using QualiFlow.Identity.API.Contracts;
using QualiFlow.Identity.Core.Entities;

namespace QualiFlow.Identity.API.Services;

public class DefaultUserCredentialsValidator : IUserCredentialsValidator
{
    private readonly ISecretHasher _secretHasher;

    public DefaultUserCredentialsValidator(ISecretHasher secretHasher)
    {
        _secretHasher = secretHasher;
    }

    public bool Validate(string username, string password, User user)
    {
        var isValidPassword = _secretHasher.VerifySecret(password, user.HashedPassword, user.HashedPasswordSalt);

        return isValidPassword;
    }
}
