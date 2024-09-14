using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QualiFlow.EntityFrameworkCore.SqlServer;
using QualiFlow.Identity.API.Contracts;
using QualiFlow.Identity.Core.Models;
using System.Threading;

namespace QualiFlow.Identity.API.API
{
    [ApiController]
    [Route("api/v1/Identity/Login")]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserCredentialsValidator _userCredentialsValidator;
        private readonly IAccessTokenIssuer _tokenIssuer;

        public LoginController(IUserCredentialsValidator userCredentialsValidator, IAccessTokenIssuer tokenIssuer, ApplicationDbContext applicationDbContext)
        {
            _userCredentialsValidator = userCredentialsValidator;
            _tokenIssuer = tokenIssuer;
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Name == request.UserName);
            if (user == null)
            {
                return new LoginResponse(false, null, null);
            }

            var valid = _userCredentialsValidator.Validate(request.UserName.Trim(), request.Password.Trim(), user);
            if (valid == false)
                return new LoginResponse(false, null, null);

            var tokens = _tokenIssuer.IssueTokens(user);

            return new LoginResponse(true, tokens.AccessToken, tokens.RefreshToken);
        }
    }
}
