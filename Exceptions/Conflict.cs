using CoreLib.Exceptions;

namespace UserService.Exceptions
{
    public class UserConflict : Conflict
    {
        public UserConflict(string email) : base(409, $"User is already exists with email: {email}") { }
    }
}