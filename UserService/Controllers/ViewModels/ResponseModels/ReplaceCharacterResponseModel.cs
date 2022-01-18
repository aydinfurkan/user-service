using System;

namespace UserService.Controllers.ViewModels.ResponseModels
{
    public class ReplaceCharacterResponseModel
    {
        public Guid CharacterId { get; set; }
        public bool Success { get; set; }
        
        public ReplaceCharacterResponseModel(Guid characterId, bool success)
        {
            CharacterId = characterId;
            Success = success;
        }
    }
}