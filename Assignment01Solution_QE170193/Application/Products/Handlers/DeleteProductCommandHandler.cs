using Application.Exceptions.Products;
using Application.Products.Commands;
using DataAccess.Interface;
using MediatR;

namespace Application.Products.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId) ?? throw new ProductNotFoundException();
            product.ProductStatus = 2;
            await _productRepository.UpdateAsync(product);
            return Unit.Value;
        }
    }
}
