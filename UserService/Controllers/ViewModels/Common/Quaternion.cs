namespace UserService.Controllers.ViewModels.Common
{
    public class Quaternion
    {
        public decimal X { set; get; }
        public decimal Y { set; get; }
        public decimal Z { set; get; }
        public decimal W { set; get; }

        public Domains.ValueObject.Quaternion ToModel()
        {
            return new (X, Y, Z, W);
        }
    }
}