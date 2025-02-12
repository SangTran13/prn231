using Application.Members.Responses;
using MediatR;

namespace Application.Members.Queries
{
    public class GetAllMembersQuery : IRequest<List<MemberResponse>>
    {

    }
}
