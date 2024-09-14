using QualiFlow.Identity.Core.Entities;

namespace QualiFlow.Identity.API.Contracts;

public interface IUserCredentialsValidator
{
    bool Validate(string username, string password, User user);
}
