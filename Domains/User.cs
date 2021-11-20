using System;
using System.Collections.Generic;
using CoreLib.Mongo;
using MongoDB.Bson.Serialization.Attributes;

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
            CharacterList = new List<Character>(5);;
        }
    }
}