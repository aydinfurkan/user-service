using System;

namespace UserService.ViewModels.ResponseModels
{
    public class CreateUserResponseModel
    {
        public string UserToken { get; set; }
        public bool Success { get; set; }
        
        public CreateUserResponseModel(string userToken, bool success)
        {
            UserToken = userToken;
            Success = success;
        }
    }
}