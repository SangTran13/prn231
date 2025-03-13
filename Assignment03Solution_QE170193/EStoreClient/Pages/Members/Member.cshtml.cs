using DataAccess.Dto.MemberDto;
using EStoreClient.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EStoreClient.Pages.Members
{
    public class MemberModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string MemberApiUrl = "";
        public List<MemberResponseDto> ListMember { get; set; }

        [BindProperty]
        public string? Keyword { get; set; }

        public MemberModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/GetAllMember";
        }
        public async Task<IActionResult> OnGetAsync(string? keyword)
        {
            var currentRole = HttpContext.Session.GetString("Role");

            if (currentRole == null || currentRole != RoleConstant.ADMIN)
            {
                return RedirectToPage("/Index");
            }

            Keyword = keyword;
            string url = MemberApiUrl + "?keyword=" + keyword;
            if (keyword == null)
            {
                url = MemberApiUrl;
            }
            HttpResponseMessage resp = await client.GetAsync(url);

            var strData = await resp.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<MemberResponseDto> listMembers = JsonSerializer.Deserialize<List<MemberResponseDto>>(strData, options);
            ListMember = listMembers;
            return Page();
        }
    }
}
