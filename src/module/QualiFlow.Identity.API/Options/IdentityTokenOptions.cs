using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace QualiFlow.Identity.API.Options;

/// <summary>
/// Represents options about token validation and generation.
/// </summary>
public class IdentityTokenOptions
{
    /// <summary>
    /// The key to use when signing tokens
    /// </summary>
    public string SigningKey { get; set; } = default!;

    /// <summary>
    /// The issuer to use when creating and validating tokens
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// The audience to use when creating and validating tokens
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// The lifetime of access tokens
    /// </summary>
    public TimeSpan AccessTokenLifetime { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    /// The lifetime of refresh tokens
    /// </summary>
    public TimeSpan RefreshTokenLifetime { get; set; } = TimeSpan.FromHours(2);

    /// <summary>
    /// Creates a new <see cref="SecurityKey"/> from the <see cref="SigningKey"/>.
    /// </summary>
    /// <returns></returns>
    public SecurityKey CreateSecurityKey() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));

    /// <summary>
    /// Configures the <see cref="JwtBearerOptions"/> with the values from this instance.
    /// </summary>
    /// <param name="options">The options to configure.</param>
    public void ConfigureJwtBearerOptions(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = CreateSecurityKey(),
            ValidAudience = Audience,
            ValidIssuer = Issuer,
            ValidateLifetime = true,
            LifetimeValidator = ValidateLifetime,
            NameClaimType = JwtRegisteredClaimNames.Name
        };
    }

    private static bool ValidateLifetime(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
    {
        return expires != null && expires > DateTime.UtcNow;
    }
}