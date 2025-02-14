using DataAccess.Services.Interface;
using eBookStoreWebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var adminEmail = _configuration["Admin:Email"];
                var adminPassword = _configuration["Admin:Password"];

                if (!string.IsNullOrEmpty(adminEmail) && !string.IsNullOrEmpty(adminPassword) &&
                    adminEmail == email && adminPassword == password)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Role = "Admin",
                        Name = "Admin",
                        Email = email,  
                        Message = "Login Admin Success"
                    });
                }

                var user = await _userService.CheckLoginAsync(email);
                if (user == null)
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "User not found"
                    });
                }

                if (user.password != password)
                {
                    return BadRequest(new
                    {
                        Status = "Error",
                        Message = "Invalid password"
                    });
                }

                return Ok(new
                {
                    Status = "Success",
                    Role = "User",
                    Name = $"{user.first_name} {user.last_name}",
                    Email = user.email_address,
                    Message = "Login User Success"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An unexpected error occurred. Please try again later."
                });
            }
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUser([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { Message = "Email is required" });
            }

            var user = await _userService.CheckLoginAsync(email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var userVM = new UserVM
            {
                Email = user.email_address,
                FirstName = user.first_name,
                Password  = user.password,
                MiddleName = user.middle_name,
                LastName = user.last_name,
                HireDate = user.hire_date,
                PublisherId = user.pub_id,
                RoleId = user.role_id,
                Source = user.source
            };

            return Ok(userVM);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromQuery] string email, [FromBody] UserVM userVM)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { Message = "Email is required" });
            }

            var user = await _userService.CheckLoginAsync(email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            user.password = userVM.Password;
            user.first_name = userVM.FirstName;
            user.middle_name = userVM.MiddleName;
            user.last_name = userVM.LastName;

            var result = await _userService.UpdateAsync(user.user_id, user);
            if (!result)
            {
                return StatusCode(500, new { Message = "Error updating user" });
            }

            return Ok(new { Message = "User updated successfully" });
        }
    }
}
