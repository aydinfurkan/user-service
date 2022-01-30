﻿using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Controllers.ViewModels.Common;
using UserService.Domains;
using Character = UserService.Controllers.ViewModels.Common.Character;

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
                Id = x.Id.ToString(),
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
                    X = x.Position.X, 
                    Y = x.Position.Y, 
                    Z = x.Position.Z
                },
                MaxHealth = x.MaxHealth,
                Health = x.Health,
                MaxMana = x.MaxMana,
                Mana = x.Mana,
            }).ToList();
        }
    }
}