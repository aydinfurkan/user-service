using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Quaternion
    {
        [BsonElement("x")] 
        public decimal X;
        [BsonElement("y")] 
        public decimal Y;
        [BsonElement("z")] 
        public decimal Z;
        [BsonElement("w")] 
        public decimal W;

        public Quaternion(decimal x, decimal y, decimal z, decimal w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}