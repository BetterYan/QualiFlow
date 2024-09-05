namespace QualiFlow.Login.Core.Entities;

public class User
{
    public string Name { get; set; }
    public string HashedPassword { get; set; }
    public string HashedPasswordSalt { get; set; }
}
