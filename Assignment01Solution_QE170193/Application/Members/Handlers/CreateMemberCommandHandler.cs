using Application.Members.Commands;
using BusinessObject;
using DataAccess.Interface;
using MediatR;

namespace Application.Members.Handlers
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, int>
    {
        private readonly IMemberRepository _memberRepository;
        public CreateMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<int> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = new Member
            {
                MemberName = request.MemberName,
                Email = request.Email,
                City = request.City,
                Country = request.Country,
                Password = request.Password,
                Role = "Member",
                DateOfBirth = request.DateOfBirth,
                Status = 1
            };

            var existingMember = await _memberRepository.GetMemberByEmailAsync(member.Email);

            if (existingMember != null)
            {
                throw new Exception("Member already exists");
            }

            await _memberRepository.AddAsync(member);
            return member.MemberId;
        }
    }
}
