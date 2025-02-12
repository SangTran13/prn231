using MediatR;

namespace Application.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public int CategoryId { get; set; }

        public DeleteCategoryCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
