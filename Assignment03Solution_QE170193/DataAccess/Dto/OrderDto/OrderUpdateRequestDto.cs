namespace DataAccess.Dto.OrderDto
{
    public class OrderUpdateRequestDto
    {
        public int OrderId { get; set; }
        public Guid MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequireDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal Freight { get; set; }
    }
}
