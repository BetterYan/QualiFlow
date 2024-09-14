using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using QualiFlow.Identity.API.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QualiFlow.Identity.API.Utility;

public static class JwtBearer
{
    public static string CreateToken(Action<JwtCreationOptions> options) => CreateToken(null, options);

    internal static string CreateToken(JwtCreationOptions options = null, Action<JwtCreationOptions> optsAction = null)
    {
        var opts = options ?? new JwtCreationOptions();
        optsAction?.Invoke(opts);

        if (string.IsNullOrEmpty(opts.SigningKey))
            throw new InvalidOperationException($"'{nameof(JwtCreationOptions.SigningKey)}' is required!");

        if (opts.SigningStyle is TokenSigningStyle.Asymmetric && opts.SigningAlgorithm is SecurityAlgorithms.HmacSha256Signature)
            throw new InvalidOperationException($"Please set an appropriate '{nameof(JwtCreationOptions.SigningAlgorithm)}' when creating Asymmetric JWTs!");

        var claimList = new List<Claim>();

        if (opts.User.Claims.Count > 0)
            claimList.AddRange(opts.User.Claims);

        if (opts.User.Permissions.Count > 0)
            claimList.AddRange(opts.User.Permissions.Select(p => new Claim("permissions", p)));

        if (opts.User.Roles.Count > 0)
            claimList.AddRange(opts.User.Roles.Select(r => new Claim("role", r)));

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = opts.Issuer,
            Audience = opts.Audience,
            IssuedAt = TimeProvider.System.GetUtcNow().UtcDateTime,
            Subject = new(claimList),
            Expires = opts.ExpireAt,
            SigningCredentials = GetSigningCredentials(opts)
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(descriptor);
    }

    static SigningCredentials GetSigningCredentials(JwtCreationOptions opts)
    {
        if (opts.SigningStyle == TokenSigningStyle.Asymmetric)
        {
            var rsa = RSA.Create();
            if (opts.KeyIsPemEncoded)
                rsa.ImportFromPem(opts.SigningKey);
            else
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(opts.SigningKey), out _);

            return new(new RsaSecurityKey(rsa), opts.SigningAlgorithm);
        }

        return new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(opts.SigningKey)), opts.SigningAlgorithm);
    }
}
