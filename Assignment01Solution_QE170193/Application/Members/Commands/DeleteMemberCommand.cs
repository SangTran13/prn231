using MediatR;

namespace Application.Members.Commands
{
    public class DeleteMemberCommand : IRequest<Unit>
    {
        public int MemberId { get; set; }

        public DeleteMemberCommand(int memberId)
        {
            MemberId = memberId;
        }
    }
}
