using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebClient.DTO.Session;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.teacher
{
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<GetSessionDTO> ListSession { get; set; }

        public IndexModel()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }
        public IActionResult OnGet()
        {
            string userJson = _httpContextAccessor.HttpContext.Session.GetString("currentUser");

            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login");
            }
            GetUserDTO u = JsonSerializer.Deserialize<GetUserDTO>(userJson);
            try
            {
                ListSession = SessionService.GetSessionByTeacher(u.Id);
            }
            catch (Exception)
            {
                return RedirectToPage("/SeverError");
            }
            return Page();
        }
    }
}
