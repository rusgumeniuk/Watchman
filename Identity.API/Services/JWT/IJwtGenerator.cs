using System.Security.Claims;

namespace Identity.API.Services.JWT
{
    public interface IJwtGenerator
    {
        string GenerateJSONWebToken(Claim[] claims = null);
    }
}
