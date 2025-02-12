using BusinessObject;
using DataTransfer;
using eStoreClient.Untils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace eStoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public HomeController(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private IActionResult RedirectToRoleBasedAction(string role)
        {
            if (role == "Admin")
                return RedirectToAction("Index", "Member");
            if (role == "Member")
                return RedirectToAction("Profile", "Member");
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (!string.IsNullOrEmpty(role))
            {
                return RedirectToRoleBasedAction(role);
            }

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest loginRequest)
        {
            try
            {
                var admin = new Member
                {
                    MemberName = "Admin",
                    Email = _configuration["Credentials:Email"],
                    Password = _configuration["Credentials:Password"],
                    Role = "Admin",
                    Status = 1
                };

                if (loginRequest.Email == admin.Email && loginRequest.Password == admin.Password)
                {
                    HttpContext.Session.SetInt32("USERID", -1);
                    HttpContext.Session.SetString("USERNAME", admin.MemberName);
                    HttpContext.Session.SetString("ROLE", admin.Role);

                    return RedirectToAction("Index", "Member");
                }

                var apiResponse = await ApiHandler.DeserializeApiResponse<List<Member>>("https://localhost:7237/api/members", HttpMethod.Get);

                var account = apiResponse.Data.FirstOrDefault(c => c.Email == loginRequest.Email && c.Password == loginRequest.Password);

                if (account != null && account.Status == 1)
                {
                    HttpContext.Session.SetInt32("USERID", account.MemberId);
                    HttpContext.Session.SetString("USERNAME", account.MemberName);
                    HttpContext.Session.SetString("ROLE", account.Role);

                    return account.Role == "Admin" ? RedirectToAction("Index", "Member") : RedirectToAction("Profile", "Member");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Email or password is invalid or account locked";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (!string.IsNullOrEmpty(role))
            {
                return RedirectToRoleBasedAction(role);
            }

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MemberRequest memberRequest)
        {
            var emailExists = await ApiHandler.CheckEmailExists(memberRequest.Email);
            if (emailExists || memberRequest.Email.Equals("admin@estore.com"))
            {
                ViewData["ErrorMessage"] = "Email already exists.";
                return View("Register");
            }

            try
            {
                var response = await ApiHandler.DeserializeApiResponse<int>("https://localhost:7237/api/members", HttpMethod.Post, memberRequest);

                if (response.StatusCode == 1000)
                {
                    TempData["SuccessMessage"] = "Registered new account successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["ErrorMessage"] = "An error occurred during registration.";
                    return View("Register");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View("Register");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
