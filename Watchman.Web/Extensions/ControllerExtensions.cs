using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Watchman.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetAccessTokenFromCookies(this Controller controller)
        {
            var token = controller.Request.Cookies["access_token"];
            return token;
        }

        public static string GetUserEmailFromHttpContext(this Controller controller)
        {
            var claim = controller.HttpContext.User.Claims.FirstOrDefault(cl => cl.Type.Contains(JwtRegisteredClaimNames.Email));
            return claim?.Value;
        }
    }
}
