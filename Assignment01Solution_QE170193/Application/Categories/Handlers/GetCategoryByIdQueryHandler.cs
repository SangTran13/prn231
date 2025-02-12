using Application.Categories.Queries;
using Application.Categories.Responses;
using Application.Exceptions.Categories;
using Application.Mappings;
using DataAccess.Interface;
using MediatR;

namespace Application.Categories.Handlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId) ?? throw new CategoryNotFoundException();

            return AppMapper<CoreMappingProfile>.Mapper.Map<CategoryResponse>(category);
        }
    }
}
