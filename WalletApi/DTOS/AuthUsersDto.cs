using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WalletApi.DTOS
{
    public class AuthUsersDto
    {
        [EmailAddress(ErrorMessage = "Unvalid email address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }   
    }
}

