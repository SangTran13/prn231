using Application.Categories.Responses;
using Application.Members.Responses;
using Application.Suppliers.Responses;

namespace Application.Products.Responses
{
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int ProductStatus { get; set; }

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }

        public CategoryResponse Category { get; set; } = null!;

        public SupplierResponse Supplier { get; set; } = null!;
    }
}
