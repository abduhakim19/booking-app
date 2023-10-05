namespace API.DTOs.Accounts
{
    public class CreateChangePasswordDto
    {
        public string Email { get; set; }
        public int Otp { get; set; }
        public string NewPasword { get; set; }
        public string ConfirmPassword { get; set; } 
    }
}
