﻿using System;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Controllers.ViewModels.Common
{
    public class Attributes
    {
        public int Strength { get; set; }
        public int Vitality { get; set; }
        public int Dexterity { get; set; }
        public int Intelligent { get; set; }
        public int Wisdom { get; set; }
        public int Defense { get; set; }
        
        public Domains.ValueObject.Attributes ToModel()
        {
            return new (Strength, Vitality, Dexterity, Intelligent, Wisdom, Defense);
        }
    }
}