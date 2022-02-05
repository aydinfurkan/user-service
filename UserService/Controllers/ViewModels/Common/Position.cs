namespace UserService.Controllers.ViewModels.Common
{
    public class Position
    {
        public decimal X;
        public decimal Y;
        public decimal Z;

        public Domains.ValueObject.Position ToModel()
        {
            return new (X, Y, Z);
        }
    }
}