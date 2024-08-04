using System.ComponentModel.DataAnnotations;

namespace StockAPI.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
