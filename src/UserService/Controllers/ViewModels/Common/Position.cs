namespace UserService.Controllers.ViewModels.Common
{
    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Domains.ValueObject.Position ToModel()
        {
            return new (X, Y, Z);
        }
    }
}