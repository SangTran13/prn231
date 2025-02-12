using Application.OrderDetails.Responses;
using MediatR;

namespace Application.OrderDetails.Queries
{
    public class GetOrderDetailByOrderIdAndProductIdQuery : IRequest<OrderDetailResponse>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public GetOrderDetailByOrderIdAndProductIdQuery(int orderId, int productId)
        {
            OrderId = orderId;
            ProductId = productId;
        }
    }
}
