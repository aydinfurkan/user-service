using CoreLib.Exceptions;

namespace UserService.Exceptions
{
    public class UserBadRequest : BadRequest
    {
        public UserBadRequest(string request) : base(400, $"{request} is not a valid request") { }
    }
}