using System;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains
{
    public class Character
    {
        [BsonElement("characterId")]
        public Guid CharacterId { get; set; }
        [BsonElement("characterName")]
        public string CharacterName { get; set; }
        
        public Character(string characterName)
        {
            CharacterId = Guid.NewGuid();
            CharacterName = characterName;
        }
    }
}