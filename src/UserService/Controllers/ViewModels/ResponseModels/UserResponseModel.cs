using System;
using System.Collections.Generic;
using System.Linq;
using RandomNameGenerator;
using UserService.Domains;
using UserService.Domains.ValueObject;
using UserService.Helpers.Extensions;
using Attributes = UserService.Controllers.ViewModels.Common.Attributes;
using Character = UserService.Controllers.ViewModels.Common.Character;
using Position = UserService.Controllers.ViewModels.Common.Position;
using Quaternion = UserService.Controllers.ViewModels.Common.Quaternion;

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
                Id = x.Id,
                Name = x.Name,
                Class = x.Class,
                Position = new Position
                {
                    X = x.Position.X, 
                    Y = x.Position.Y, 
                    Z = x.Position.Z
                },
                Quaternion = new Quaternion()
                {
                    X = x.Quaternion.X, 
                    Y = x.Quaternion.Y, 
                    Z = x.Quaternion.Z,
                    W = x.Quaternion.W
                },
                Attributes = new Attributes()
                {
                    Strength = x.Attributes.Strength,
                    Intelligent = x.Attributes.Intelligent,
                    Dexterity = x.Attributes.Dexterity,
                    Defense = x.Attributes.Defense,
                    Vitality = x.Attributes.Vitality,
                    Wisdom = x.Attributes.Wisdom
                },
                Experience = x.Experience
            }).ToList();
        }

        public static UserResponseModel Random()
        {
            var name = NameGenerator.GenerateFirstName(Gender.Male);
            var surname = NameGenerator.GenerateLastName();
            var email = $"{name}@pkhood.com";
            var user = new User(name, surname, email);
            
            foreach (var c in Class.All.Shuffle())
            {
                user.AddCharacter(new Domains.Character($"{name[0]}{name.Substring(1).ToLower()}.{c.Name}", c.Name));
            }
            
            user.Id ??= Guid.NewGuid();
            
            return new UserResponseModel(user);
        }
    }
}