using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.teacher
{
    public class GradedModel : PageModel
    {

        public List<GetUserDTO> ListUserDTO { get; set; }

        public int SessionId { get; set; }
        public string CourseName { get; set; }
        public string ClassName { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Msg { get; set; }

        public IActionResult OnGet(int sessionId, string className, string courseName, int gradeId, string gradeName)
        {
            try
            {
                GetData(sessionId, className, courseName, gradeId, gradeName);

            }
            catch (Exception)
            {

                return RedirectToPage("/SeverError");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(List<int> studentIds, List<decimal> grades, int sessionId, string className, string courseName, int gradeId, string gradeName)
        {
            int countFalse = 0;
            for (int i = 0; i < studentIds.Count; i++)
            {
                bool success;
                try
                {
                    success = StudentGradeService.GradedForStudent(gradeId, studentIds[i], grades[i]);
                }
                catch (Exception)
                {
                    return RedirectToPage("/SeverError");
                }
                 ;
                if (!success)
                {
                    countFalse++;
                }
            }
            if (countFalse > 0)
            {
                Msg = $"Graded fail. The {gradeName} of {courseName} - {className} has been graded before";
            }
            else
            {
                Msg = "Graded success";
            }


            try
            {
                GetData(sessionId, className, courseName, gradeId, gradeName);

            }
            catch (Exception)
            {
                return RedirectToPage("/SeverError");
            }

            return Page();
        }

        public void GetData(int sessionId, string className, string courseName, int gradeId, string gradeName)
        {
            SessionId = sessionId;
            CourseName = courseName;
            ClassName = className;
            GradeId = gradeId;
            GradeName = gradeName;


            ListUserDTO = UserService.GetStudentInSession(sessionId);
        }
    }
}
