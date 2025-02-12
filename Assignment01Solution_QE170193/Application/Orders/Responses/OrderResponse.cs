using Application.Members.Responses;

namespace Application.Orders.Responses
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public decimal Total { get; set; }

        public int OrderStatus { get; set; }

        public string Freight { get; set; } = string.Empty;

        public int MemberId { get; set; }

        public MemberResponse Member { get; set; } = null!;

    }
}
