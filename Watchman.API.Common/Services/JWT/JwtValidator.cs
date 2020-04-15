using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

using Watchman.BusinessLogic.Models.Data;

namespace Watchman.API.Common.Services.JWT
{
    public class JwtValidator : IJwtValidator
    {
        private readonly IConfiguration _configuration;
        public JwtValidator(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public bool ValidateToken(string authToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                tokenHandler.ValidateToken(authToken, validationParameters, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public string GetClaimValueFromToken(string authToken, string claimType)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out SecurityToken validatedToken);

                Claim resultClaim = (principal as ClaimsPrincipal).Claims.FirstOrDefault(claim => claim.Type.Contains(claimType) || claim.Type.Equals(claimType));

                return resultClaim?.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])) // The same key as the one that generate the token
            };
        }

        public ClaimsPrincipal GetClaimsPrincipal(string authToken)
        {
            throw new NotImplementedException();
        }
    }
}
