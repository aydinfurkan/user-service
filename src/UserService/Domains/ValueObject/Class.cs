using System.Collections.Generic;

namespace UserService.Domains.ValueObject
{
    public class Class
    {
        public static Class Mage = new ("mage");
        public static Class Warrior = new ("warrior");
        public static Class Archer = new ("archer");
        public static Class Healer = new ("healer");

        public static List<Class> All = new (){Mage, Warrior, Archer, Healer};
        public string Name { get; }

        private Class(string name)
        {
            Name = name;
        }
    }
}