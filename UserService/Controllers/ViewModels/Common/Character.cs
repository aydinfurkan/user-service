namespace UserService.Controllers.ViewModels.Common
{
    public class Character
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public Position Position { get; set; }
        public Quaternion Quaternion { get; set; }
        public decimal MaxHealth { get; set; }
        public decimal Health { get; set; }
        public decimal MaxMana { get; set; }
        public decimal Mana { get; set; }
    }
}