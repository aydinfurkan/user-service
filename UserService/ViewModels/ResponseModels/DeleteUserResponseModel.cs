using System;

namespace UserService.ViewModels.ResponseModels
{
    public class DeleteUserResponseModel
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        
        public DeleteUserResponseModel(Guid id, bool success)
        {
            Id = id;
            Success = success;
        }
    }
}