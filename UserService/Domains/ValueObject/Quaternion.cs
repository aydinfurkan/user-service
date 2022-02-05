using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Quaternion
    {
        [BsonElement("x")] 
        public decimal X { get; set; }
        [BsonElement("y")] 
        public decimal Y { get; set; }
        [BsonElement("z")] 
        public decimal Z { get; set; }
        [BsonElement("w")] 
        public decimal W { get; set; }

        public Quaternion(decimal x, decimal y, decimal z, decimal w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}