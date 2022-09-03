namespace UserService.Controllers.ViewModels.ResponseModels
{
    public class AuthorizeUserResponseModel
    {
        public string PToken { get; set; }
        public bool Success { get; set; }
        
        public AuthorizeUserResponseModel(string pToken, bool success)
        {
            PToken = pToken;
            Success = success;
        }
    }
}