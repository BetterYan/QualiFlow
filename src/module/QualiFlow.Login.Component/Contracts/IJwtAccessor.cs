namespace QualiFlow.Login.Component.Contracts;

public interface IJwtAccessor
{
    ValueTask<string> ReadTokenAsync(string name);
    ValueTask WriteTokenAsync(string name, string token);
}
