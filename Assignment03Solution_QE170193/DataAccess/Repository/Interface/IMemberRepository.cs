using DataAccess.Dto.MemberDto;

namespace DataAccess.Repository.Interface
{
    public interface IMemberRepository
    {
        Task DeleteMember(Guid userId);
        Task<MemberResponseDto?> GetMemberById(Guid id);
        Task<List<MemberResponseDto>?> GetMembers(string? keyword);
        Task<MemberResponseDto?> Login(string email, string password);
        Task Register(MemberRequestDto member);
        Task SaveMember(MemberRequestDto request);
        Task UpdateMember(MemberUpdateRequest request);
        Task UpdateMemberPassword(Guid userId, string newPassword);
        Task<bool> CheckOldPassword(Guid userId, string oldPassword);
    }
}
