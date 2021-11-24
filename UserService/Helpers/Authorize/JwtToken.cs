using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CoreLib.Exceptions;
using Microsoft.IdentityModel.Tokens;
using UserService.Configs;
using UserService.Domains;
using UserService.Exceptions;

namespace UserService.Helpers.Authorize
{
    public class JwtToken : IToken
    {
        private readonly SymmetricSecurityKey _mySecurityKey;
        private readonly string _algorithm;
        private readonly JwtTokenSettings _jwtTokenSettings;
        private const string ClaimsEmail = "Email";
        private const string ClaimsUserId = "UserId";

        public JwtToken(JwtTokenSettings jwtTokenSettings)
        {
            _jwtTokenSettings = jwtTokenSettings;
            _algorithm = SecurityAlgorithms.HmacSha256Signature;
            _mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenSettings.SecretKey));
        }
        
        public string CreateToken(User user)
        {
            try
            {
                var now = DateTime.UtcNow;

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimsUserId, user.Id.ToString()),
                    new Claim(ClaimsEmail, user.Email)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor 
                {
                    Issuer = _jwtTokenSettings.Issuer,
                    Audience = _jwtTokenSettings.Audience,
                    Subject = new ClaimsIdentity(claims),
                    Expires = now.AddDays(1),
                    SigningCredentials  = new SigningCredentials(_mySecurityKey, _algorithm)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw new UserServerError($"Jwt Token Create Error. {e.Message}");
            }
        }
        
        public PTokenClaims GetClaims(ClaimsPrincipal claimsPrincipal)
        {
            var jtiOk = Guid.TryParse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value, out var jti);
            var idOk = Guid.TryParse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimsUserId)?.Value, out var id);
            var email = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimsEmail)?.Value;

            if (!jtiOk || !idOk)
            {
                throw new UserServerError($"Jwt Token GetClaims Parse Guid Error.");
            }

            return new PTokenClaims(jti, id, email);
        }
        public PTokenClaims ParseToken(string pToken)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidAudience = _jwtTokenSettings.Audience,
                ValidIssuer = _jwtTokenSettings.Issuer,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenSettings.SecretKey))
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken validatedPToken = null;
            ClaimsPrincipal claimsPrincipal = null;
            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(pToken, validationParameters, out validatedPToken);
            }
            catch
            {
               throw new UserBadRequest(pToken);
            }

            return GetClaims(claimsPrincipal);
            
        }
    }
}