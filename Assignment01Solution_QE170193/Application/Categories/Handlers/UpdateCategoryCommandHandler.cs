using Application.Categories.Commands;
using Application.Exceptions.Categories;
using DataAccess.Interface;
using MediatR;

namespace Application.Categories.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId) ?? throw new CategoryNotFoundException();

            category.CategoryName = request.CategoryName;
            category.Description = request.Description;

            await _categoryRepository.UpdateAsync(category);
            return category.CategoryId;
        }
    }
}
