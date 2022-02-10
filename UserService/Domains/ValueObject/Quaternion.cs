using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Quaternion
    {
        [BsonElement("x")] 
        public double X { get; set; }
        [BsonElement("y")] 
        public double Y { get; set; }
        [BsonElement("z")] 
        public double Z { get; set; }
        [BsonElement("w")] 
        public double W { get; set; }

        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}