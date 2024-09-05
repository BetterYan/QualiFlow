using QualiFlow.Login.Component.Contracts;
using QualiFlow.Login.Component.Models;

namespace QualiFlow.Login.Component.Services;

public class DefaultCredentialsValidator : ICredentialsValidator
{
    public ValueTask<ValidateCredentialsResult> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
