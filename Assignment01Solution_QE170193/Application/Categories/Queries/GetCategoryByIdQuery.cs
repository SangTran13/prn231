using Application.Categories.Responses;
using MediatR;

namespace Application.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryResponse>
    {
        public int CategoryId { get; set; }

        public GetCategoryByIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
