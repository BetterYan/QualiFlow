using QualiFlow.Identity.Core.Entities;
using QualiFlow.Identity.Core.Models;

namespace QualiFlow.Identity.API.Contracts;

public interface IAccessTokenIssuer
{
    IssuedTokens IssueTokens(User user);
}
