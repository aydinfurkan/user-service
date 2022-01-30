using System;
using FluentValidation;
using UserService.Controllers.ViewModels.Common;

namespace UserService.Controllers.ViewModels.RequestModels
{
    public class ReplaceCharacterRequestModel
    {
        public Guid CharacterId { get; set; }
        public Position Position { get; set; }
        public Quaternion Quaternion { get; set; }
        public decimal MaxHealth { get; set; }
        public decimal Health { get; set; }
        public decimal MaxMana { get; set; }
        public decimal Mana { get; set; }
    }
    public class ReplaceCharacterRequestModelValidator : AbstractValidator<ReplaceCharacterRequestModel>
    {
        public ReplaceCharacterRequestModelValidator()
        {
            
        }
    }
}