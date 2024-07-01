using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebClient.DTO.Request;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.student
{
    public class ViewApplicationModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<GetRequestDTO> ListRequest { get; set; }

        public ViewApplicationModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            string userJson = _httpContextAccessor.HttpContext.Session.GetString("currentUser");
            GetUserDTO u = JsonSerializer.Deserialize<GetUserDTO>(userJson);

            ListRequest = RequestService.GetRequestByStudent(u.Id);
            return Page();
        }
    }
}
