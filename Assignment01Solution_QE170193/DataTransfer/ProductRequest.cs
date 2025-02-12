using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class ProductRequest
    {
        public int ProductId { get; set; }
        [Required, StringLength(40)]
        public string ProductName { get; set; } = string.Empty;
        [Required, StringLength(40)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int UnitsInStock { get; set; }
        [Required]
        public int FlowerBouquetStatus { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int SupplierId { get; set; }
    }
}
