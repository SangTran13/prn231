using eBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace eBookStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient client;
        private readonly string UserApiUri = "https://localhost:7158/api/User";

        public UsersController()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                TempData["LoginFail"] = "Email and password cannot be empty.";
                return RedirectToAction(nameof(Login));
            }

            try
            {
                HttpResponseMessage response = await client.PostAsync($"{UserApiUri}/login?email={email}&password={password}", null);
                string strData = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(strData))
                {
                    TempData["LoginFail"] = "Server error. Please try again later.";
                    return RedirectToAction(nameof(Login));
                }

                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(strData);
                if (json == null || !json.ContainsKey("role") || !json.ContainsKey("email"))
                {
                    TempData["LoginFail"] = "Wrong email or password";
                    return RedirectToAction(nameof(Login));
                }

                string role = json["role"].ToString();
                string userEmail = json["email"].ToString();
                string name = json["name"].ToString();

                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("Email", userEmail);
                HttpContext.Session.SetString("Username", name);

                ViewBag.Role = role; 

                if (role == "Admin")
                {
                    return RedirectToAction("ViewBook", "Books");
                }
                return RedirectToAction("Profile", "Users");
            }
            catch
            {
                TempData["LoginFail"] = "An error occurred. Please try again.";
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            string email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(nameof(Login));
            }

            ViewBag.Role = HttpContext.Session.GetString("Role"); 
            ViewBag.Username = HttpContext.Session.GetString("Username");
            HttpResponseMessage response = await client.GetAsync($"{UserApiUri}/profile?email={email}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["ProfileFail"] = "Cannot load profile.";
                return RedirectToAction(nameof(Login));
            }

            string json = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserViewModel>(json);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            string email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(nameof(Login));
            }

            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.Username = HttpContext.Session.GetString("Username");

            HttpResponseMessage response = await client.GetAsync($"{UserApiUri}/profile?email={email}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["ProfileFail"] = "Cannot load profile.";
                return RedirectToAction(nameof(Profile));
            }

            string json = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserViewModel>(json);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel user)
        {
            string email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction(nameof(Login));
            }

            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.Username = HttpContext.Session.GetString("Username");

            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"{UserApiUri}/update?email={email}", content);
            if (!response.IsSuccessStatusCode)
            {
                TempData["UpdateFail"] = "Failed to update profile.";
                return RedirectToAction(nameof(EditProfile));
            }

            TempData["UpdateSuccess"] = "Profile updated successfully!";
            HttpContext.Session.SetString("Username", $"{user.FirstName} {user.LastName}");
            return RedirectToAction(nameof(Profile));
        }
    }
}
