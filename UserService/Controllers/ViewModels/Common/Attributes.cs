using System;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Controllers.ViewModels.Common
{
    public class Attributes
    {
        public int Strength;
        public int Vitality;
        public int Dexterity;
        public int Intelligent;
        public int Wisdom;
        public int Defense;
        
        public Domains.ValueObject.Attributes ToModel()
        {
            return new (Strength, Vitality, Dexterity, Intelligent, Wisdom, Defense);
        }
    }
}