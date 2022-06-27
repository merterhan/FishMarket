namespace FishMarket.Dto
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }        
        public bool ConfirmPassword { get; set; }
    }
}
