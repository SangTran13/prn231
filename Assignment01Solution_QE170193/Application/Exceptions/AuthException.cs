using Shared.Constants;

namespace Application.Exceptions
{
    public class AuthException : Exception
    {
        public StatusCode StatusCode { get; }
        public IEnumerable<string>? Errors { get; }

        public AuthException(StatusCode statusCode, IEnumerable<string>? errors = null)
            : base(ResponseMessages.GetMessage(statusCode))
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
