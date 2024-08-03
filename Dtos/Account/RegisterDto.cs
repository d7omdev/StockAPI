using System.ComponentModel.DataAnnotations;

namespace StockAPI.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "'ConfirmPassword' and 'Password' do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}
