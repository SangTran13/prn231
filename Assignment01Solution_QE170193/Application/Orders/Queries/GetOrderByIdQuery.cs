using Application.Orders.Responses;
using MediatR;

namespace Application.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public int OrderId { get; set; }

        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
