using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domains.ValueObject
{
    public class Position
    {
        [BsonElement("x")] 
        public double X { get; set; }

        [BsonElement("y")] 
        public double Y { get; set; }

        [BsonElement("z")] 
        public double Z { get; set; }

        public Position(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}