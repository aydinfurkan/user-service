namespace UserService.Controllers.ViewModels.Common
{
    public class Quaternion
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }
        public decimal W { get; set; }

        public Domains.ValueObject.Quaternion ToModel()
        {
            return new (X, Y, Z, W);
        }
    }
}