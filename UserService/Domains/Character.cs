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
        [BsonElement("characterClass")]
        public string CharacterClass { get; set; }
        
        public Character(string characterName, string characterClass)
        {
            CharacterId = Guid.NewGuid();
            CharacterName = characterName;
            CharacterClass = characterClass;
        }
    }
}