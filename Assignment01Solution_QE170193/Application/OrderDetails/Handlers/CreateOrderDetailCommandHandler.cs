using Application.Exceptions.Orders;
using Application.Exceptions.Products;
using Application.OrderDetails.Commands;
using BusinessObject;
using DataAccess.Interface;
using MediatR;

namespace Application.OrderDetails.Handlers
{
    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, Unit>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public CreateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            _ = await _orderRepository.GetByIdAsync(request.OrderId) ?? throw new OrderNotFoundException();
            _ = await _productRepository.GetByIdAsync(request.ProductId) ?? throw new ProductNotFoundException();

            var orderDetail = new OrderDetail
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity,
                Discount = request.Discount
            };
            await _orderDetailRepository.AddAsync(orderDetail);
            return Unit.Value;
        }
    }
}
