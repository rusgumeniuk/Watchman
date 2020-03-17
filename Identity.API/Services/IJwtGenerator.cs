using System.Security.Claims;

namespace Identity.API.Services
{
    public interface IJwtGenerator
    {
        string GenerateJSONWebToken(Claim[] claims = null);
    }
}
