using Application.Members.Responses;
using MediatR;

namespace Application.Members.Queries
{
    public class GetMemberByIdQuery : IRequest<MemberResponse>
    {
        public int MemberId { get; set; }
        public GetMemberByIdQuery(int memberId)
        {
            MemberId = memberId;
        }
    }
}
