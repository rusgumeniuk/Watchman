using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IJwtValidator
    {
        bool ValidateToken(string token);
        TokenValidationParameters GetValidationParameters();
        string GetClaimValueFromToken(string authToken, string claimType);
        ClaimsPrincipal GetClaimsPrincipal(string authToken);
    }
}
