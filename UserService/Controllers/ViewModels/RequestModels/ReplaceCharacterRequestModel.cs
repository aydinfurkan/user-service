using System;
using FluentValidation;
using UserService.Controllers.ViewModels.Common;
using Position = UserService.Controllers.ViewModels.Common.Position;
using Quaternion = UserService.Controllers.ViewModels.Common.Quaternion;

namespace UserService.Controllers.ViewModels.RequestModels
{
    public class ReplaceCharacterRequestModel
    {
        public Guid CharacterId { get; set; }
        public Position Position { get; set; }
        public Quaternion Quaternion { get; set; }
        public Attributes Attributes { get; set; }
        public decimal Experience { get; set; }
    }
    public class ReplaceCharacterRequestModelValidator : AbstractValidator<ReplaceCharacterRequestModel>
    {
        public ReplaceCharacterRequestModelValidator()
        {
            
        }
    }
}