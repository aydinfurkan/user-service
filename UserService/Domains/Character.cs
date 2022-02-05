using System;
using MongoDB.Bson.Serialization.Attributes;
using UserService.Domains.ValueObject;

namespace UserService.Domains
{
    public class Character
    {
        [BsonElement("id")]
        public Guid Id;
        [BsonElement("name")] 
        public string Name;
        [BsonElement("class")] 
        public string Class;
        [BsonElement("position")]
        public Position Position;
        [BsonElement("quaternion")] 
        public Quaternion Quaternion;
        [BsonElement("attributes")] 
        public Attributes Attributes;
        [BsonElement("experience")] 
        public decimal Experience;

        public Character(string characterName, string characterClass)
        {
            Id = Guid.NewGuid();
            Name = characterName;
            Class = characterClass;
            Position = new Position(70, 45, 0);
            Quaternion = new Quaternion(0, 0, 0, 0);
            Attributes = Attributes.GetAttributesFromClass(characterClass);
            Experience = 0;
        }
    }
}