using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eStoreAPI.Models
{
    public class CreateOrderRequest
    {
        [JsonRequired]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Freight is required.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Freight must be between 1 and 40 characters.")]
        public string Freight { get; set; } = string.Empty;

        [JsonRequired]
        public int MemberId { get; set; }
    }
}
