namespace QualiFlow.Identity.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HashedPassword { get; set; }
    public string HashedPasswordSalt { get; set; }
}
