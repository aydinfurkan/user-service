using System.Security.Claims;
using UserService.Domains;

namespace UserService.Authorize
{
    public interface IToken
    {
        public string CreateToken(User user);
        public UserTokenClaims GetClaims(ClaimsPrincipal claimsPrincipal);
    }
}