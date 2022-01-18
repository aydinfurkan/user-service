namespace UserService.Controllers.ViewModels.Common
{
    public class Position
    {
        public int X { set; get; }
        public int Y { set; get; }
        public int Z { set; get; }

        public Domains.ValueObject.Position ToModel()
        {
            return new (X, Y, Z);
        }
    }
}