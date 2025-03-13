using EStoreClient.Dto.MemberDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EStoreClient.Pages.Members
{
    public class AddMemberModel : PageModel
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        private string _addMemberApiUrl;

        [BindProperty]
        public MemberAddRequest MemberAddRequest { get; set; }
        public AddMemberModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _addMemberApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/AddMember";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = new MemberAddRequest
            {
                Email = MemberAddRequest.Email,
                FirstName = MemberAddRequest.FirstName,
                LastName = MemberAddRequest.LastName,
                PhoneNumber = MemberAddRequest.PhoneNumber,
                Password = MemberAddRequest.Password
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PostAsync(_addMemberApiUrl, httpContent);

            // Read the response from the API
            string responseContent = await response.Content.ReadAsStringAsync();

            return RedirectToPage("/Members/Member");
        }
    }
}
