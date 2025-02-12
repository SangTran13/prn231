using BusinessObject;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class OrderItemRequest
    {
        [Required]
        public Product Product { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal? Discount { get; set; } = 0;
    }
}
