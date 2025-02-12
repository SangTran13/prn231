using Application.Exceptions.Members;
using Application.Mappings;
using Application.Members.Queries;
using Application.Members.Responses;
using DataAccess.Interface;
using MediatR;

namespace Application.Members.Handlers
{
    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<MemberResponse> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId) ?? throw new MemberNotFoundException();

            return AppMapper<CoreMappingProfile>.Mapper.Map<MemberResponse>(member);

        }
    }
}
