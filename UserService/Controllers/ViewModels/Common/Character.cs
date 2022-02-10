using System;

namespace UserService.Controllers.ViewModels.Common
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public Position Position { get; set; }
        public Quaternion Quaternion { get; set; }
        public Attributes Attributes { get; set; }
        public double Experience { get; set; }
    }
}