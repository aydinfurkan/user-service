using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Domains;

namespace UserService.Controllers.ViewModels.ResponseModels
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<Character> CharacterList { get; set; }

        public UserResponseModel(User user)
        {
            Id = (Guid) user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            CharacterList = user.CharacterList.Select(x => new Character
            {
                CharacterId = x.CharacterId.ToString(),
                CharacterName = x.CharacterName,
                CharacterClass = x.CharacterClass
            }).ToList();
        }
    }

    public class Character
    {
        public string CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string CharacterClass { get; set; }
    }
}