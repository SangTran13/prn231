namespace EStoreClient.Dto.MemberDto
{
    public class MemberAddRequest
    {
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
