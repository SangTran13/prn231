using Application.Exceptions.Members;
using Application.Members.Commands;
using DataAccess.Interface;
using MediatR;

namespace Application.Members.Handlers
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Unit>
    {
        private readonly IMemberRepository _memberRepository;
        public UpdateMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Unit> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId) ?? throw new MemberNotFoundException();

            member.MemberName = request.MemberName;
            member.City = request.City;
            member.Country = request.Country;
            member.Password = request.Password;
            member.DateOfBirth = request.DateOfBirth;
            member.Status = request.Status;

            await _memberRepository.UpdateAsync(member);
            return Unit.Value;
        }
    }
}
