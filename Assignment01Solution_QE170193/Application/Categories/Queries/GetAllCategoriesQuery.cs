using Application.Categories.Responses;
using MediatR;

namespace Application.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryResponse>>
    {

    }
}
