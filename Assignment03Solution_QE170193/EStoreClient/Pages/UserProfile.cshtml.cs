using DataAccess.Dto.MemberDto;
using EStoreClient.Dto.MemberDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace EStoreClient.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        private string ProfileApiUrl = "";
        private string UpdateProfileApiUrl = "";
        private string ChangePasswordApiUrl = "";

        [BindProperty]
        public UserProfileViewModel Profile { get; set; }

        [BindProperty]
        public ChangePasswordRequest ChangePasswordRequest { get; set; }

        public UserProfileModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProfileApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/Detail/{id}";
            UpdateProfileApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/Update";
            ChangePasswordApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/ChangePassword";
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            string requestUrl = ProfileApiUrl.Replace("{id}", id);
            HttpResponseMessage res = await client.GetAsync(requestUrl);
            var strData = await res.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var member = JsonSerializer.Deserialize<MemberResponseDto>(strData, options);

            Profile = new UserProfileViewModel()
            {
                ProfileId = member.Id,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                PhoneNumber = member.PhoneNumber
            };

            return Page();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            if (ChangePasswordRequest.NewPassword != ChangePasswordRequest.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "New password and confirmation do not match.";
                return RedirectToPage("UserProfile", new { id = ChangePasswordRequest.MemberId });
            }

            ChangePasswordRequest.MemberId = Guid.Parse(HttpContext.Session.GetString("MemberId") ?? Guid.Empty.ToString());

            var content = new StringContent(
                JsonSerializer.Serialize(ChangePasswordRequest),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await client.PutAsync(ChangePasswordApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Password changed successfully.";
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = errorMessage;
            }

            return RedirectToPage("UserProfile", new { id = ChangePasswordRequest.MemberId });
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync()
        {
            var memberId = HttpContext.Session.GetString("MemberId");

            if (string.IsNullOrEmpty(memberId))
            {
                TempData["ErrorMessage"] = "User session expired. Please log in again.";
                return RedirectToPage("/Login");
            }

            Profile.ProfileId = Guid.Parse(memberId);

            var updateRequest = new MemberUpdateRequestDto
            {
                MemberId = Profile.ProfileId,
                Email = Profile.Email,
                FirstName = Profile.FirstName,
                LastName = Profile.LastName,
                PhoneNumber = Profile.PhoneNumber
            };

            var content = new StringContent(
                JsonSerializer.Serialize(updateRequest),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await client.PutAsync(UpdateProfileApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Profile updated successfully.";
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = errorMessage;
            }

            return RedirectToPage("UserProfile", new { id = Profile.ProfileId });
        }

    }
}