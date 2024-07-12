using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimetableSystem.Services;
using WebClient.DTO.Course;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.admin
{
    public class IndexModel : PageModel
    {
        public List<CourseDTO> ListCourse { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }
        public IActionResult OnGet()
        {
            GetUserDTO user = AuthenticationHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext);

            if (user == null || !AuthenticationHelper.IsAdmin(user))
            {
                return Redirect("/AccessDenied");
            }

            try
            {
                ListCourse = CourseService.GetCourses();
            }
            catch
            {
                return Redirect("/SeverError");
            }

            return Page();
        }
    }
}
