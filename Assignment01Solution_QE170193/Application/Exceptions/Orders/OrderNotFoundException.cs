using Shared.Constants;

namespace Application.Exceptions.Orders
{
    public class OrderNotFoundException : AppException
    {
        public OrderNotFoundException() : base(StatusCode.OrderNotFound) { }
    }
}
