using DataAccess.Dto.MemberDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace EStoreClient.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        private string _loginApiUrl;

        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _loginApiUrl = _configuration.GetValue<string>("DomainURL") + "Member/Login";
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = new
            {
                Email,
                Password
            };

            string body = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Make the POST request using HttpClient with the request body
            HttpResponseMessage response = await client.PostAsync(_loginApiUrl, httpContent);

            // if bad request show error message
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                ViewData["Title"] = "Your account is locked out after multiple login attempts. Please try again later.";
                return Page();
            }

            // Read the response from the API
            string responseContent = await response.Content.ReadAsStringAsync();

            var memberResponse = JsonConvert.DeserializeObject<MemberResponseDto>(responseContent);

            if (memberResponse != null)
            {

                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("MemberId", memberResponse.Id.ToString());
                HttpContext.Session.SetString("Email", memberResponse.Email);
                HttpContext.Session.SetString("Role", memberResponse.Email.Equals("admin@estore.com") ? "ADMIN" : "USER");
                return RedirectToPage("/Index");
            }
            else
            {
                ViewData["Title"] = "Incorrect email or password !";
            }
            return Page();
        }
    }
}
