using Application.Exceptions.Categories;
using Application.Exceptions.Products;
using Application.Exceptions.Suppliers;
using Application.Products.Commands;
using DataAccess.Interface;
using MediatR;

namespace Application.Products.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId) ?? throw new ProductNotFoundException();
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId) ?? throw new CategoryNotFoundException();
            var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId) ?? throw new SupplierNotFoundException();

            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.UnitPrice = request.UnitPrice;
            product.UnitsInStock = request.UnitsInStock;
            product.ProductStatus = request.ProductStatus;
            product.Category = category;
            product.Supplier = supplier;

            await _productRepository.UpdateAsync(product);
            return Unit.Value;
        }

    }
}
