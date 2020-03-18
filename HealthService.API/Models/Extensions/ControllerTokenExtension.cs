﻿using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace HealthService.API.Models.Extensions
{
    public static class ControllerTokenExtension
    {
        public static string GetBearerTokenFromRequest(this Controller controller)
        {
            var header = controller.Request.Headers.Values
                .FirstOrDefault(header => header.ToString().Contains("Bearer"));
            var token = header.ToString().Remove(0, 7);
            return token;
        }

        public static string GetUserEmailFromHttpContext(this Controller controller)
        {
            var claim = controller.HttpContext.User.Claims.FirstOrDefault(cl => cl.Type.Contains(JwtRegisteredClaimNames.Email));
            return claim.Value;            
        }
    }
}
