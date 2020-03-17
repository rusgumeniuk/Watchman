using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Services
{
    public interface IJwtValidator
    {
        bool ValidateToken(string token);
        TokenValidationParameters GetValidationParameters();
    }
}
