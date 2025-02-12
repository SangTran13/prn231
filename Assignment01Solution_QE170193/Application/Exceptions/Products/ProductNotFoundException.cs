using Shared.Constants;

namespace Application.Exceptions.Products
{
    public class ProductNotFoundException : AppException
    {
        public ProductNotFoundException() : base(StatusCode.ProductNotFound) { }
    }
}
