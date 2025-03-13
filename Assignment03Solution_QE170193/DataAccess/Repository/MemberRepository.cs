using BusinessObject.Models;
using DataAccess.Dto.MemberDto;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly UserManager<Member> _userManager;

        public MemberRepository(UserManager<Member> userManager)
        {
            _userManager = userManager;
        }

        public async Task DeleteMember(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<MemberResponseDto?> GetMemberById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new MemberResponseDto
            {
                Id = Guid.Parse(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault()
            };
        }

        public async Task<List<MemberResponseDto>?> GetMembers(string? keyword)
        {
            keyword = keyword?.ToUpper();

            var users = await _userManager.Users
                .Where(u => string.IsNullOrEmpty(keyword) || (u.FirstName.ToUpper().Contains(keyword) || u.LastName.ToUpper().Contains(keyword) || u.Email!.ToUpper().Contains(keyword)))
                .ToListAsync();

            return users.Select(user => new MemberResponseDto
            {
                Id = Guid.Parse(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault()
            }).ToList();
        }

        public async Task<MemberResponseDto?> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

          /*  await _userManager.AccessFailedAsync(user);

            if (await _userManager.IsLockedOutAsync(user))
            {
                throw new Exception("Account is locked out, please try again later");
            }*/

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                {
                    throw new Exception("Account is locked out, please try again later");
                }

                return null;
            }


            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var roles = await _userManager.GetRolesAsync(user);

                return new MemberResponseDto
                {
                    Id = Guid.Parse(user.Id),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = roles.FirstOrDefault()
                };
            }
            return null;
        }

        public async Task Register(MemberRequestDto member)
        {
            var user = new Member
            {
                UserName = member.Email,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                PhoneNumber = member.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, member.Password);

            // Add role
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");
            }

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }

        public async Task SaveMember(MemberRequestDto request)
        {
            var user = new Member
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");
            }

            if (!result.Succeeded)
            {
                throw new Exception("Saving member failed");
            }
        }

        public async Task UpdateMember(MemberUpdateRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PhoneNumber = request.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Update failed");
                }
            }
        }

        public async Task UpdateMemberPassword(Guid userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (!result.Succeeded)
                {
                    throw new Exception("Password update failed");
                }
            }
        }

        public async Task<bool> CheckOldPassword(Guid userId, string oldPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.CheckPasswordAsync(user, oldPassword);
            return result;
        }
    }
}
