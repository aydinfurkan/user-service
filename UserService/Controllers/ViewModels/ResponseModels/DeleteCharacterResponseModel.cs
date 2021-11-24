using System;

namespace UserService.Controllers.ViewModels.ResponseModels
{
    public class DeleteCharacterResponseModel
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        
        public DeleteCharacterResponseModel(Guid id, bool success)
        {
            Id = id;
            Success = success;
        }
    }
}