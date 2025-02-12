using Application.Orders.Responses;
using MediatR;

namespace Application.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<List<OrderResponse>>
    {

    }
}
