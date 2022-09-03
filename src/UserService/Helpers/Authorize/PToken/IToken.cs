using System.Security.Claims;
using UserService.Domains;

namespace UserService.Helpers.Authorize.PToken
{
    public interface IToken
    {
        public string CreateToken(User user);
        public PTokenClaims GetClaims(ClaimsPrincipal claimsPrincipal);
        public PTokenClaims ParseToken(string pToken);
    }
}