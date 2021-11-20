using CoreLib.Exceptions;

namespace UserService.Exceptions
{
    public class UserNotFound : NotFound
    {
        public UserNotFound(string email) : base(404, $"User not found with email: {email}") { }
    }
}