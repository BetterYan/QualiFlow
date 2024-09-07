using QualiFlow.Identity.Component.Models;

namespace QualiFlow.Identity.Component.Contracts;

public interface ICredentialsValidator
{
    ValueTask<ValidateCredentialsResult> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken = default
        );
}
