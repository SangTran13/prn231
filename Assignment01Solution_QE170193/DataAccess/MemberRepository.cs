using BusinessObject;
using BusinessObjects;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MemberRepository : RepositoryBase<Member, int>, IMemberRepository
    {
        public MemberRepository(EstoreDbContext context) : base(context)
        {

        }

        public async Task<Member?> GetMemberByEmailAsync(string email)
        {
            return await _context.Member.FirstOrDefaultAsync(m => m.Email == email);
        }
    }
}
