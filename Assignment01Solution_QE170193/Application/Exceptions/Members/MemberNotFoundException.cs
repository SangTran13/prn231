using Shared.Constants;

namespace Application.Exceptions.Members
{
    public class MemberNotFoundException : AppException
    {
        public MemberNotFoundException() : base(StatusCode.MemberNotFound)
        {
        }
    }
}
