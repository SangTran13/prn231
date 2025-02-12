using Application.Exceptions.Members;
using Application.Exceptions.Orders;
using Application.Mappings;
using Application.Orders.Queries;
using Application.Orders.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Orders.Handlers
{
    public class GetOrdersByMemberQueryHandler : IRequestHandler<GetOrdersByMemberQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMemberRepository _memberRepository;

        public GetOrdersByMemberQueryHandler(IOrderRepository orderRepository, IMemberRepository memberRepository)
        {
            _orderRepository = orderRepository;
            _memberRepository = memberRepository;
        }

        public async Task<List<OrderResponse>> Handle(GetOrdersByMemberQuery request, CancellationToken cancellationToken)
        {
            _ = await _memberRepository.GetByIdAsync(request.MemberId) ?? throw new MemberNotFoundException();
            var order = await _orderRepository.GetOrdersByMemberIdAsync(request.MemberId) ?? throw new OrderNotFoundException();

            return AppMapper<CoreMappingProfile>.Mapper.Map<List<OrderResponse>>(order);
        }
    }
}
