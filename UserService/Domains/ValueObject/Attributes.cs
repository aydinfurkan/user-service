using System;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Attributes
    {
        public static Attributes Mage = new (R(), R(), R(), R(), R(), R());
        public static Attributes Warrior = new (R(), R(), R(), R(), R(), R());
        public static Attributes Archer = new (R(), R(), R(), R(), R(), R());
        public static Attributes Healer = new (R(), R(), R(), R(), R(), R());
        
        [BsonElement("strength")] 
        public int Strength;
        [BsonElement("vitality")] 
        public int Vitality;
        [BsonElement("dexterity")] 
        public int Dexterity;
        [BsonElement("intelligent")] 
        public int Intelligent;
        [BsonElement("wisdom")] 
        public int Wisdom;
        [BsonElement("defense")] 
        public int Defense;

        public Attributes(int strength, int vitality, int dexterity, int intelligent, int wisdom, int defense)
        {
            Strength = strength;
            Vitality = vitality;
            Dexterity = dexterity;
            Intelligent = intelligent;
            Wisdom = wisdom;
            Defense = defense;
        }
        
        public static Attributes GetAttributesFromClass(string className)
        {
            return className switch
            {
                "mage" => Mage,
                "archer" => Archer,
                "warrior" => Warrior,
                "healer" => Healer,
                _ => Warrior
            };
        }   

        public static Random Rand = new Random();
        public static int R()
        {
            Rand ??= new Random();
            lock(Rand)
            {
                return Rand.Next(5,20);
            }
        }
    }
}