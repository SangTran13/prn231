namespace DataAccess.Dto.MemberDto
{
    public class MemberUpdateRequest
    {
        public Guid MemberId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
