
using Authorization.Common.Models.Response;
using System.Security.Claims;

namespace Authorization.Common.Services.Authentication
{
    public interface IAuthenticationServices
    {
        UserResponse GenerateTokens(IEnumerable<Claim> claims);
    }
}
