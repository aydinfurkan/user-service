using System;
using MongoDB.Bson.Serialization.Attributes;
using UserService.Domains.ValueObject;

namespace UserService.Domains
{
    public class Character
    {
        [BsonElement("id")]
        public Guid Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("class")]
        public string Class { get; set; }
        [BsonElement("position")] 
        public Position Position { get; set; }
        [BsonElement("position")] 
        public Quaternion Quaternion { get; set; }
        [BsonElement("maxHealth")] 
        public decimal MaxHealth { get; set; }
        [BsonElement("health")] 
        public decimal Health { get; set; }
        [BsonElement("maxMana")] 
        public decimal MaxMana { get; set; }
        [BsonElement("mana")] 
        public decimal Mana { get; set; }

        public Character(string characterName, string characterClass)
        {
            Id = Guid.NewGuid();
            Name = characterName;
            Class = characterClass;
            Position = new Position(0, 10, 0);
            Quaternion = new Quaternion(0, 0, 0, 0);
            MaxHealth = 1000;
            Health = 1000;
            MaxMana = 100;
            Mana = 100;
        }
    }
}