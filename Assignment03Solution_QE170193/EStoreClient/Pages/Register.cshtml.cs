using EStoreClient.Dto.MemberDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace EStoreClient.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient client = null;
        private readonly IConfiguration _configuration;
        private string RegisterApiUrl = "";

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public RegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            RegisterApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/Register";
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = new MemberAddRequest
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Password = Password
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PostAsync(RegisterApiUrl, httpContent);

            // Read the response from the API
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                ViewData["Title"] = responseContent;
                return Page();
            }

            ViewData["Title"] = "Registration successful";
            return RedirectToPage("/Login");
        }
    }
}
