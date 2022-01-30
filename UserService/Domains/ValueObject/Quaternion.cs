using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Quaternion
    {
        [BsonElement("x")] 
        public decimal X { private set; get; }
        [BsonElement("y")] 
        public decimal Y { private set; get; }
        [BsonElement("z")] 
        public decimal Z { private set; get; }
        [BsonElement("w")] 
        public decimal W { private set; get; }

        public Quaternion(decimal x, decimal y, decimal z, decimal w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}