using System;
using System.Collections.Generic;
using System.Linq;
using CoreLib.Mongo;
using MongoDB.Bson.Serialization.Attributes;
using UserService.Exceptions;

namespace UserService.Domains
{
    public class User : SoftDeleteDocument
    {
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("surname")]
        public string Surname { get; set; }
        
        [BsonElement("email")]
        public string Email { get; set; }
        
        [BsonElement("characterList")]
        public List<Character> CharacterList { get; set; }

        public User(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
            CharacterList = new List<Character>(5);
        }

        public void AddCharacter(Character character)
        {
            if (CharacterList.Count >= 5) throw new CharacterConflict();
            CharacterList.Add(character);
        }
        public void DeleteCharacter(Guid characterId)
        {
            if (CharacterList.Count == 0) throw new CharacterConflict();
            var character = CharacterList.FirstOrDefault(x => x.CharacterId == characterId);
            if (character == null) throw new CharacterNotFound(characterId);
            CharacterList.Remove(character);
        }
    }
}