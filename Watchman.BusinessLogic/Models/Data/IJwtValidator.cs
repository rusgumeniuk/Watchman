using Microsoft.IdentityModel.Tokens;

namespace Watchman.BusinessLogic.Models.Data
{
    public interface IJwtValidator
    {
        bool ValidateToken(string token);
        TokenValidationParameters GetValidationParameters();
    }
}
