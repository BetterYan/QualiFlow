using Microsoft.AspNetCore.Mvc;
using QualiFlow.EntityFrameworkCore.SqlServer;
using QualiFlow.Identity.Core.Models;

namespace QualiFlow.Identity.API.API;

[ApiController]
[Route("api/v1/Identity/User")]
public class IdentityUserController : ControllerBase
{
    readonly ApplicationDbContext _dbContext;

    public IdentityUserController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult CreateUser(RegisterRequest request)
    {
        var password = request.Password.Trim();

        return Ok("CreateUser");
    }
}
