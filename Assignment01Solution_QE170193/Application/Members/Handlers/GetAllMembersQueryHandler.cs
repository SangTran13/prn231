using Application.Mappings;
using Application.Members.Queries;
using Application.Members.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Members.Handlers
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, List<MemberResponse>>
    {
        private readonly IMemberRepository _memberRepository;

        public GetAllMembersQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<List<MemberResponse>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await _memberRepository.GetAllAsync();

            return AppMapper<CoreMappingProfile>.Mapper.Map<List<MemberResponse>>(members);
        }
    }
}
