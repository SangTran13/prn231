using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EStoreClient.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.Remove("IsLoggedIn");
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}
