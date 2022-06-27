using FishMarket.Core;

namespace FishMarket.Entities.Concrete
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
