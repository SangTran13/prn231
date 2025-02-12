using MediatR;

namespace Application.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string CategoryName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
