using Microsoft.AspNetCore.Mvc;
using QualiFlow.EntityFrameworkCore.SqlServer;
using QualiFlow.Identity.API.Contracts;
using QualiFlow.Identity.Core.Entities;
using QualiFlow.Identity.Core.Models;

namespace QualiFlow.Identity.API.API;

[ApiController]
[Route("api/v1/Identity/User")]
public class IdentityUserController : ControllerBase
{
    readonly ApplicationDbContext _dbContext;
    readonly ISecretHasher _secretHasher;

    public IdentityUserController(ApplicationDbContext dbContext, ISecretHasher secretHasher)
    {
        _dbContext = dbContext;
        _secretHasher = secretHasher;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(RegisterRequest request)
    {
        var password = request.Password.Trim();
        var hashedPassword = _secretHasher.HashSecret(password);

        var user = new User
        {
            Name = request.UserName,
            HashedPassword = hashedPassword.EncodeSecret(),
            HashedPasswordSalt = hashedPassword.EncodeSalt(),
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
