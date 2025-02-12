using Application.OrderDetails.Responses;
using MediatR;

namespace Application.OrderDetails.Queries
{
    public class GetAllOrderDetailsQuery : IRequest<List<OrderDetailResponse>>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
