namespace UserService.Controllers.ViewModels.Common
{
    public class Quaternion
    {
        public decimal X;
        public decimal Y;
        public decimal Z;
        public decimal W;

        public Domains.ValueObject.Quaternion ToModel()
        {
            return new (X, Y, Z, W);
        }
    }
}