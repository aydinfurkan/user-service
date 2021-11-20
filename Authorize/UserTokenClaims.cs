using System;

namespace UserService.Authorize
{
    public class UserTokenClaims
    {
        public Guid Jti { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        
        
        public UserTokenClaims(Guid jti, Guid userId, string email)
        {
            Jti = jti;
            UserId = userId;
            Email = email;
        }
    }
}
