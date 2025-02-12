using Application.Exceptions.Categories;
using Application.Exceptions.Suppliers;
using Application.Products.Commands;
using BusinessObject;
using DataAccess.Interface;
using MediatR;

namespace Application.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId) ?? throw new CategoryNotFoundException();
            var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId) ?? throw new SupplierNotFoundException();

            var product = new Product
            {
                ProductName = request.ProductName,
                Description = request.Description,
                UnitPrice = request.UnitPrice,
                UnitsInStock = request.UnitsInStock,
                ProductStatus = 1,
                Category = category,
                Supplier = supplier
            };
            await _productRepository.AddAsync(product);
            return product.ProductId;
        }
    }
}
