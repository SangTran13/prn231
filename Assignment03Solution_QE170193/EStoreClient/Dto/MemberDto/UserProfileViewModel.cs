namespace EStoreClient.Dto.MemberDto
{
    public class UserProfileViewModel
    {
        public Guid ProfileId { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
