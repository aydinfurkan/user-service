namespace UserService.Controllers.ViewModels.Common
{
    public class Position
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }

        public Domains.ValueObject.Position ToModel()
        {
            return new (X, Y, Z);
        }
    }
}