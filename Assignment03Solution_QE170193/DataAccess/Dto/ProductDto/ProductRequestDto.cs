namespace DataAccess.Dto.ProductDto
{
    public class ProductRequestDto
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
    }
}
