using Shared.Constants;

namespace Application.Exceptions.Categories
{
    public class CategoryNotFoundException : AppException
    {
        public CategoryNotFoundException() : base(StatusCode.CategoryNotFound) { }
    }
}
