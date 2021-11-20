using CoreLib.Exceptions;

namespace UserService.Exceptions
{
    public class UserServerError : ServerError
    {
        public UserServerError(string message = null) : base(message) { }
    }
}