using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Position
    {
        [BsonElement("x")] 
        public decimal X { get; set; }

        [BsonElement("y")] 
        public decimal Y { get; set; }

        [BsonElement("z")] 
        public decimal Z { get; set; }

        public Position(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}