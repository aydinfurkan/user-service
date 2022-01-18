using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Position
    {
        [BsonElement("x")] 
        public decimal X { private set; get; }
        [BsonElement("y")] 
        public decimal Y { private set; get; }
        [BsonElement("z")] 
        public decimal Z { private set; get; }

        public Position(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}