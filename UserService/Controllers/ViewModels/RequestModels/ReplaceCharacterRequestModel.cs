using System;
using FluentValidation;
using UserService.Controllers.ViewModels.Common;

namespace UserService.Controllers.ViewModels.RequestModels
{
    public class ReplaceCharacterRequestModel
    {
        public Guid CharacterId { get; set; }
        public Position Position { get; set; }
        public int Health { get; set; }
    }
    public class ReplaceCharacterRequestModelValidator : AbstractValidator<ReplaceCharacterRequestModel>
    {
        public ReplaceCharacterRequestModelValidator()
        {
            
        }
    }
}