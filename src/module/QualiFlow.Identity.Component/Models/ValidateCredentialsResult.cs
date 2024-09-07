namespace QualiFlow.Identity.Component.Models;

public record ValidateCredentialsResult(bool IsAuthenticated, string AccessToken, string RefreshToken);

