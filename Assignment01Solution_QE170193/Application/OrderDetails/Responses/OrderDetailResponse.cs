using Application.Products.Responses;

namespace Application.OrderDetails.Responses
{
    public class OrderDetailResponse
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public ProductResponse Product { get; set; } = null!;
    }
}
