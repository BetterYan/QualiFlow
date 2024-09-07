using QualiFlow.Identity.Component.Contracts;
using QualiFlow.Identity.Component.Models;

namespace QualiFlow.Identity.Component.Services;

public class DefaultCredentialsValidator : ICredentialsValidator
{
    public ValueTask<ValidateCredentialsResult> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
