using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WalletApi.Domain;

namespace WalletApi.DTOS
{
    public class TradeRequestDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public Users User { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string TokenToTrade { get; set; }
        [Required]
        public double AmountToTrade { get; set; }
        [Required]
        public string TokenToReceive { get; set; }
        [Required]
        public double AmountToReceive { get; set; }
        [JsonIgnore]
        public string Status = "pending";

    }
}
