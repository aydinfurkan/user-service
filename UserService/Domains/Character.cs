﻿using System;
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
        [BsonElement("health")] 
        public int Health { get; set; }

        public Character(string characterName, string characterClass)
        {
            Id = Guid.NewGuid();
            Name = characterName;
            Class = characterClass;
            Position = new Position(0, 0, 0);
            Health = 1000;
        }
    }
}