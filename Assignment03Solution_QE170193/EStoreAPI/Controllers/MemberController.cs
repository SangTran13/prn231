using DataAccess.Dto.MemberDto;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _userRepository;

        public MemberController(IMemberRepository memberRepository)
        {
            _userRepository = memberRepository;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<MemberResponseDto?>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _userRepository.Login(request.Email, request.Password);
                if (user == null)
                {
                    return NotFound(null);
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] MemberRequestDto member)
        {
            try
            {
                await _userRepository.Register(member);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllMember")]
        public async Task<ActionResult<IEnumerable<MemberResponseDto>>> GetMembers([FromQuery] string? keyword)
        {
            var members = await _userRepository.GetMembers(keyword);
            return Ok(members);
        }

        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember([FromBody] MemberRequestDto p)
        {
            try
            {
                await _userRepository.SaveMember(p);
                return NoContent();
            }
            catch (Exception)
            {
                return Ok(null);
            }
        }

        [HttpGet("Detail/{id}")]
        public async Task<ActionResult<MemberResponseDto>> GetMemberById(Guid id)
        {
            var member = await _userRepository.GetMemberById(id);
            if (member == null)
            {
                return NotFound(null);
            }
            return Ok(member);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteMember(Guid id)
        {
            var member = await _userRepository.GetMemberById(id);
            if (member == null)
            {
                return NotFound(null);
            }
            await _userRepository.DeleteMember(id);
            return NoContent();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateMember([FromBody] MemberUpdateRequest p)
        {
            var member = await _userRepository.GetMemberById(p.MemberId);
            if (member == null)
            {
                return NotFound(null);
            }

            await _userRepository.UpdateMember(p);
            return NoContent();
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await _userRepository.GetMemberById(request.MemberId);

            if (user == null)
            {
                return NotFound(null);
            }

            var isOldPasswordValid = await _userRepository.CheckOldPassword(request.MemberId, request.OldPassword);
            if (!isOldPasswordValid)
            {
                return BadRequest("Old password is not correct");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest("New password and confirmation do not match");
            }

            await _userRepository.UpdateMemberPassword(request.MemberId, request.NewPassword);

            return NoContent();
        }
    }
}
