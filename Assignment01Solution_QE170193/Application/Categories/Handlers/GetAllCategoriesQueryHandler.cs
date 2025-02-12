using Application.Categories.Queries;
using Application.Categories.Responses;
using Application.Mappings;
using DataAccess.Interface;
using MediatR;

namespace Application.Categories.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoryResponses = AppMapper<CoreMappingProfile>.Mapper.Map<List<CategoryResponse>>(categories);

            return categoryResponses;
        }
    }
}
