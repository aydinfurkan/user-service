using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Position
    {
        [BsonElement("x")] 
        public int X { private set; get; }
        [BsonElement("y")] 
        public int Y { private set; get; }
        [BsonElement("z")] 
        public int Z { private set; get; }

        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}