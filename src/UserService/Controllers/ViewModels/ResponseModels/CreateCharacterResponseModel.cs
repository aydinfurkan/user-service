using System;

namespace UserService.Controllers.ViewModels.ResponseModels
{
    public class CreateCharacterResponseModel
    {
        public Guid CharacterId { get; set; }
        public bool Success { get; set; }
        
        public CreateCharacterResponseModel(Guid characterId, bool success)
        {
            CharacterId = characterId;
            Success = success;
        }
    }
}