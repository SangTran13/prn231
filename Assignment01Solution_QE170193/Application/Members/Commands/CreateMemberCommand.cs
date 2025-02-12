using MediatR;

namespace Application.Members.Commands
{
    public class CreateMemberCommand : IRequest<int>
    {
        public string MemberName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int Status { get; set; } = 1;
    }
}
