using Shared.Constants;


namespace Application.Exceptions.Suppliers
{
    public class SupplierNotFoundException : AppException
    {
        public SupplierNotFoundException() : base(StatusCode.SupplierNotFound) { }
    }
}
