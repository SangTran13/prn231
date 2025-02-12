namespace Shared.Constants
{
    public static class ResponseMessages
    {
        private static readonly Dictionary<StatusCode, string> _messages = new Dictionary<StatusCode, string>
        {
            // Success messages
            { StatusCode.RequestProcessedSuccessfully, "Request processed successfully." },

            // Error messages
            { StatusCode.ModelInvalid, "Model is invalid. Please check the request body." },
            { StatusCode.RequestProcessingFailed, "The request processing has failed." },
            { StatusCode.CategoryNotFound, "Category not found." },
            { StatusCode.SupplierNotFound, "Supplier not found." },
            { StatusCode.OrderNotFound, "Order not found." },
            { StatusCode.MemberNotFound, "Member not found." },
            { StatusCode.ProductNotFound, "Product not found." },
            { StatusCode.OrderDetailNotFound, "Order detail not found" }
        };

        public static string GetMessage(StatusCode code) => _messages[code];
    }
}
