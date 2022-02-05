using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Position
    {
        [BsonElement("x")] 
        public decimal X;

        [BsonElement("y")] 
        public decimal Y;

        [BsonElement("z")] 
        public decimal Z;

        public Position(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}