using Application.Mappings;
using Application.Orders.Queries;
using Application.Orders.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Orders.Handlers
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrders();

            var orderResponses = AppMapper<CoreMappingProfile>.Mapper.Map<List<OrderResponse>>(orders);

            return orderResponses;
        }

    }
}
