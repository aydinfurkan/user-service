using System;

namespace UserService.Helpers.Authorize.PToken
{
    public class PTokenClaims
    {
        public Guid Jti { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        
        
        public PTokenClaims(Guid jti, Guid userId, string email)
        {
            Jti = jti;
            UserId = userId;
            Email = email;
        }
    }
}
