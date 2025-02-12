namespace eStoreAPI.Models
{
    public class UpdateProductRequest
    {
        public string ProductName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int ProductStatus { get; set; } = 1;

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }
    }
}
