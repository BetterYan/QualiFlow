using QualiFlow.Login.Component.Models;

namespace QualiFlow.Login.Component.Contracts;

public interface ICredentialsValidator
{
    ValueTask<ValidateCredentialsResult> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken = default
        );
}
