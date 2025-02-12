using BusinessObject;

namespace DataAccess.Interface
{
    public interface IMemberRepository : IAsyncRepository<Member, int>
    {
        Task<Member?> GetMemberByEmailAsync(string email);
    }
}
