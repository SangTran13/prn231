using Application.Exceptions.OrderDetails;
using Application.Exceptions.Orders;
using Application.Exceptions.Products;
using Application.OrderDetails.Commands;
using DataAccess.Interface;
using MediatR;

namespace Application.OrderDetails.Handlers
{
    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, Unit>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            _ = await _orderRepository.GetByIdAsync(request.OrderId) ?? throw new OrderNotFoundException();
            _ = await _productRepository.GetByIdAsync(request.ProductId) ?? throw new ProductNotFoundException();

            var orderDetail = await _orderDetailRepository.GetOrderDetailByOrderIdAndProductIdAsync(request.OrderId, request.ProductId) ?? throw new OrderDetailNotFoundException();

            orderDetail.UnitPrice = request.UnitPrice;
            orderDetail.Quantity = request.Quantity;
            orderDetail.Discount = request.Discount;

            await _orderDetailRepository.UpdateAsync(orderDetail);
            return Unit.Value;
        }
    }

}
