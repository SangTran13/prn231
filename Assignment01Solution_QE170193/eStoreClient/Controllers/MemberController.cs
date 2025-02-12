using BusinessObject;
using DataTransfer;
using eStoreClient.Untils;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Net.Http.Headers;

namespace eStoreClient.Controllers
{
    public class MemberController : Controller
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public MemberController(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private IActionResult RedirectIfNotAuthorized(string roleRequired, string redirectToAction)
        {
            string role = HttpContext.Session.GetString("ROLE");

            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != roleRequired)
            {
                TempData["ErrorMessage"] = $"You don't have permission to access this page.";
                return RedirectToAction(redirectToAction, "Member");
            }

            return null;
        }

        [HttpGet]
        public IActionResult Create()
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MemberRequest memberRequest)
        {
            var emailExists = await ApiHandler.CheckEmailExists(memberRequest.Email);
            if (emailExists || memberRequest.Email.Equals("admin@estore.com"))
            {
                ViewData["ErrorMessage"] = "Email already exists.";
                return View("Create");
            }

            var response = await ApiHandler.DeserializeApiResponse<int>("https://localhost:7237/api/members", HttpMethod.Post, memberRequest);

            if (response.StatusCode == 1000)
            {
                TempData["SuccessMessage"] = "Creation new account successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["ErrorMessage"] = "An error occurred during creation.";
                return View("Create");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string role = HttpContext.Session.GetString("ROLE");

            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }

            // Fetch the existing member data from the API
            var apiResponse = await ApiHandler.DeserializeApiResponse<Member>($"https://localhost:7237/api/members/{id}", HttpMethod.Get);
            Member member = apiResponse.Data;

            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MemberRequest memberRequest)
        {

            int? currentUserId = HttpContext.Session.GetInt32("USERID");

            if (ModelState.IsValid)
            {
                var apiResponse = await ApiHandler.DeserializeApiResponse<ApiResponse<object>>($"https://localhost:7237/api/members/{memberRequest.MemberId}", HttpMethod.Put, memberRequest);

                if (apiResponse.StatusCode == 1000)
                {
                    if (currentUserId == memberRequest.MemberId)
                    {
                        HttpContext.Session.SetString("USERNAME", memberRequest.MemberName);
                    }
                    TempData["SuccessMessage"] = "Profile updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = apiResponse.Message ?? "An error occurred while updating the profile.";
                    return View(memberRequest);
                }
            }
            else
            {
                return View(memberRequest);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var redirectResult = RedirectIfNotAuthorized("Admin", "Profile");
            if (redirectResult != null)
                return redirectResult;

            var apiResponse = await ApiHandler.DeserializeApiResponse<List<Member>>("https://localhost:7237/api/members", HttpMethod.Get);
            var listMembers = apiResponse.Data;

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(listMembers);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var redirectResult = RedirectIfNotAuthorized("Member", "Index");
            if (redirectResult != null)
                return redirectResult;

            int userId = HttpContext.Session.GetInt32("USERID").Value;

            var apiResponse = await ApiHandler.DeserializeApiResponse<Member>($"https://localhost:7237/api/members/{userId}", HttpMethod.Get);
            Member member = apiResponse.Data;

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(member);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var redirectResult = RedirectIfNotAuthorized("Member", "Index");
            if (redirectResult != null)
                return redirectResult;

            int userId = HttpContext.Session.GetInt32("USERID").Value;

            var apiResponse = await ApiHandler.DeserializeApiResponse<Member>($"https://localhost:7237/api/members/{userId}", HttpMethod.Get);
            Member member = apiResponse.Data;

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(MemberRequest memberRequest)
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "User is not logged in.";
                return RedirectToAction("Index", "Home");
            }

            memberRequest.MemberId = userId.Value;

            var apiResponse = await ApiHandler.DeserializeApiResponse<ApiResponse<object>>($"https://localhost:7237/api/members/{userId}", HttpMethod.Put, memberRequest);

            if (apiResponse.StatusCode == 1000) // assuming 1000 is a success code
            {
                TempData["SuccessMessage"] = "Profile updated successfully.";
                HttpContext.Session.SetString("USERNAME", memberRequest.MemberName);
            }
            else
            {
                TempData["ErrorMessage"] = apiResponse.Message ?? "An error occurred while updating your profile.";
            }

            return RedirectToAction("Profile");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Member");
            }


            var apiResponse = await ApiHandler.DeserializeApiResponse<Member>("https://localhost:7237/api/members/" + id, HttpMethod.Get);
            var member = apiResponse.Data;
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction("Index");
            }

            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Member member)
        {
            _ = await ApiHandler.DeserializeApiResponse<Product>("https://localhost:7237/api/members/" + member.MemberId, HttpMethod.Delete);
            TempData["SuccessMessage"] = "Member deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
