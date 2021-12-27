using System.Linq;
using FluentValidation;
using UserService.Domains.ValueObject;

namespace UserService.Controllers.ViewModels.RequestModels
{
    public class CreateCharacterRequestModel
    {
        public string CharacterName { get; set; }
        public string CharacterClass { get; set; }
    }
    public class CreateCharacterRequestModelValidator : AbstractValidator<CreateCharacterRequestModel>
    {
        public CreateCharacterRequestModelValidator()
        {
            RuleFor(x => x.CharacterClass).Must(x => Class.All.Select(y => y.Name).Contains(x.ToLower())).WithMessage("Invalid character class.");
        }
    }
}