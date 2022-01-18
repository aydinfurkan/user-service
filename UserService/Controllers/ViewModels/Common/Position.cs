namespace UserService.Controllers.ViewModels.Common
{
    public class Position
    {
        public decimal X { set; get; }
        public decimal Y { set; get; }
        public decimal Z { set; get; }

        public Domains.ValueObject.Position ToModel()
        {
            return new (X, Y, Z);
        }
    }
}