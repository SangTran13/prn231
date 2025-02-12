using Application.Exceptions.Members;
using Application.Members.Commands;
using DataAccess.Interface;
using MediatR;

namespace Application.Members.Handlers
{
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Unit>
    {
        private readonly IMemberRepository _memberRepository;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Unit> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId) ?? throw new MemberNotFoundException();
            member.Status = 2;
            await _memberRepository.UpdateAsync(member);
            return Unit.Value;
        }
    }
}
