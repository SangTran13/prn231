using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eStoreAPI.Models
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Product name must be between 1 and 40 characters.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 255 characters.")]
        public string Description { get; set; } = string.Empty;

        [JsonRequired]
        public decimal UnitPrice { get; set; }

        [JsonRequired]
        public int UnitsInStock { get; set; }

        [JsonRequired]
        public int CategoryId { get; set; }

        [JsonRequired]
        public int SupplierId { get; set; }
    }
}
