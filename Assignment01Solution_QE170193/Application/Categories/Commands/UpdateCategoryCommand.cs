using MediatR;

namespace Application.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<int>
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
