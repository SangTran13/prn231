using Application.OrderDetails.Responses;
using MediatR;

namespace Application.OrderDetails.Queries
{
    public class GetOrderDetailsByOrderIdQuery : IRequest<List<OrderDetailResponse>>
    {
        public int OrderId { get; set; }

        public GetOrderDetailsByOrderIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
