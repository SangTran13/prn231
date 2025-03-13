namespace DataAccess.Dto.OrderDto
{
    public class OrderDetailUpdateRequestDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? Discount { get; set; }
    }
}
