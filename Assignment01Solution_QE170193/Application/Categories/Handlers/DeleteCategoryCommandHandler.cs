using Application.Categories.Commands;
using Application.Exceptions.Categories;
using DataAccess.Interface;
using MediatR;

namespace Application.Categories.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId) ?? throw new CategoryNotFoundException();

            await _categoryRepository.DeleteAsync(category);
            return Unit.Value;
        }
    }
}
