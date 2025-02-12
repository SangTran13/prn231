using Application.Categories.Commands;
using BusinessObject;
using DataAccess.Interface;
using MediatR;

namespace Application.Categories.Handlers
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateSupplierCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            var category = new Category
            {
                CategoryName = request.CategoryName,
                Description = request.Description
            };

            await _categoryRepository.AddAsync(category);
            return category.CategoryId;
        }
    }
}
