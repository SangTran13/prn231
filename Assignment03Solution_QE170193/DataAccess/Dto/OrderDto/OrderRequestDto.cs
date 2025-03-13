namespace DataAccess.Dto.OrderDto
{
    public class OrderRequestDto
    {
        public Guid MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequireDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; } = [];
    }
}
