using Microsoft.AspNetCore.Mvc;
using QualiFlow.Login.Core.Models;

namespace QualiFlow.Login.API.API;

[ApiController]
[Route("api/v1/Identity/Users")]
public class IdentityUserController : ControllerBase
{


    [HttpPost]
    public IActionResult Users(CreateUserRequest request)
    {


        return Ok("Users");
    }
}
