namespace DataAccess.Dto.MemberDto
{
    public class ChangePasswordRequest
    {
        public Guid MemberId { get; set; }
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
