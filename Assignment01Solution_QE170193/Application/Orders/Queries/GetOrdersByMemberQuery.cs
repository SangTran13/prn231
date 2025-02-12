using Application.Orders.Responses;
using MediatR;

namespace Application.Orders.Queries
{
    public class GetOrdersByMemberQuery : IRequest<List<OrderResponse>>
    {
        public int MemberId { get; set; }
        public GetOrdersByMemberQuery(int memberId)
        {
            MemberId = memberId;
        }
    }
}
