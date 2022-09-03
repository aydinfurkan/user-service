namespace UserService.Controllers.ViewModels.ResponseModels
{
    public class CreateUserResponseModel
    {
        public string PToken { get; set; }
        public bool Success { get; set; }
        
        public CreateUserResponseModel(string pToken, bool success)
        {
            PToken = pToken;
            Success = success;
        }
    }
}