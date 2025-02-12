using Shared.Constants;


namespace Application.Exceptions.OrderDetails
{
    public class OrderDetailNotFoundException : AppException
    {
        public OrderDetailNotFoundException() : base(StatusCode.OrderDetailNotFound) { }
    }
}
