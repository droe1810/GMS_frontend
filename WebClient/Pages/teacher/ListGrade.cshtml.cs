using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.DTO.Grade;
using WebClient.DTO.Session;
using WebClient.Services;

namespace WebClient.Pages.teacher
{
    public class ListGradeModel : PageModel
    {
        public List<GetGradeDTO> ListGrade { get; set; }
        public int SessionId { get; set; }
        public string CourseName { get; set; }
        public string ClassName { get; set; }
        public IActionResult OnGet(int sessionId,  string className, string courseName)
        {
            try
            {
                ListGrade = GradeService.GetGradesBySessionGradedByTeacher(sessionId); ;

            }
            catch (Exception)
            {

                return RedirectToPage("/SeverError");
            }

            SessionId = sessionId;
            ClassName = className;
            CourseName = courseName;

            return Page();
        }
    }
}
