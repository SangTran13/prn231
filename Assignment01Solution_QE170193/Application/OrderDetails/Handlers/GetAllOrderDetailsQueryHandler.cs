using Application.Mappings;
using Application.OrderDetails.Queries;
using Application.OrderDetails.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.OrderDetails.Handlers
{
    public class GetAllOrderDetailsQueryHandler : IRequestHandler<GetAllOrderDetailsQuery, List<OrderDetailResponse>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public GetAllOrderDetailsQueryHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<List<OrderDetailResponse>> Handle(GetAllOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();

            return AppMapper<CoreMappingProfile>.Mapper.Map<List<OrderDetailResponse>>(orderDetails);
        }

    }
}
