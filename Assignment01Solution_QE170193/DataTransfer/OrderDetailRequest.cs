using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class OrderDetailRequest
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Discount { get; set; }
    }
}
