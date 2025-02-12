namespace Shared.Constants
{
    public enum StatusCode
    {
        /// <summary>
        /// Success Codes
        /// </summary>
        RequestProcessedSuccessfully = 1000,

        /// <summary>
        /// Error Codes 2xxx
        /// </summary>
        RequestProcessingFailed = 2000,
        ModelInvalid = 2001,
        CategoryNotFound = 2002,
        SupplierNotFound = 2003,
        OrderNotFound = 2004,
        MemberNotFound = 2005,
        ProductNotFound = 2006,
        OrderDetailNotFound = 2007
    }
}
