namespace UserService.Controllers.ViewModels.Common
{
    public class Quaternion
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public Domains.ValueObject.Quaternion ToModel()
        {
            return new (X, Y, Z, W);
        }
    }
}