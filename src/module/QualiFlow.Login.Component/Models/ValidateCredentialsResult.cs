namespace QualiFlow.Login.Component.Models;

public record ValidateCredentialsResult(bool IsAuthenticated, string AccessToken, string RefreshToken);

