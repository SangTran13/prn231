using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace EStoreClient.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string MemberDeleteApiUrl = "";
        public DeleteMemberModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberDeleteApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/Delete/{id}";
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var memberId = HttpContext.Session.GetString("MemberId");

            if (id != memberId)
            {
                string requestUrl = MemberDeleteApiUrl.Replace("{id}", id);
                HttpResponseMessage resp = await client.DeleteAsync(requestUrl);
            }

            return RedirectToPage("Member");
        }
    }
}
