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
        public int Strength { get; set; }
        [BsonElement("vitality")] 
        public int Vitality { get; }
        [BsonElement("dexterity")] 
        public int Dexterity { get; set; }
        [BsonElement("intelligent")] 
        public int Intelligent { get; set; }
        [BsonElement("wisdom")] 
        public int Wisdom { get; set; }
        [BsonElement("defense")] 
        public int Defense { get; set; }

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