using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WalletApi.DTOS
{
    public class UsersDto
    {
        [JsonIgnore]
        public int? Id { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Unvalid email address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
