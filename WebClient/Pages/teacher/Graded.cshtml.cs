using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using TimetableSystem.Services;
using WebClient.DTO.StudentGrade;
using WebClient.DTO.User;
using WebClient.Services;
using Newtonsoft.Json;
namespace WebClient.Pages.teacher
{
    public class GradedModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GradedModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<GetUserDTO> ListUserDTO { get; set; }

        public List<StudentGradeFromJson> ListStudentGradeFromJson { get; set; }

        public int SessionId { get; set; }
        public string CourseName { get; set; }
        public string ClassName { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Msg { get; set; }

        public IActionResult OnGet(int sessionId, string className, string courseName, int gradeId, string gradeName)
        {
            GetUserDTO user = AuthenticationHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext);

            if (user == null || !AuthenticationHelper.IsTeacher(user))
            {
                return Redirect("/AccessDenied");
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

        public IActionResult OnPostGraded(List<int> studentIds, List<decimal> grades, int sessionId, string className, string courseName, int gradeId, string gradeName)
        {
            GetUserDTO user = AuthenticationHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext);

            if (user == null || !AuthenticationHelper.IsTeacher(user))
            {
                return Redirect("/AccessDenied");
            }

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


        public IActionResult OnPostImportFile(IFormFile file, int sessionId, string className, string courseName, int gradeId, string gradeName)
        {
            GetData(sessionId, className, courseName, gradeId, gradeName);
            if (file == null)
            {
                Msg = "No input file";
                return Page();
            }

            using (var r = new StreamReader(file.OpenReadStream()))
            {
                string json = r.ReadToEnd();

                try
                {
                    ListStudentGradeFromJson = System.Text.Json.JsonSerializer.Deserialize<List<StudentGradeFromJson>>(json);
                }
                catch
                {
                    Msg = "File data is not in the correct format";
                    return Page();
                }
                for (int i = 0; i < ListStudentGradeFromJson.Count; i++)
                {
                    if ((!ListStudentGradeFromJson[i].studentName.ToLower().Equals(ListUserDTO[i].Username.ToLower()))
                        || (ListStudentGradeFromJson[i].gradeValue > 10)
                        || (ListStudentGradeFromJson[i].gradeValue < 0)
                        )
                    {
                        ListStudentGradeFromJson = null;
                        Msg = "Invalid file data";
                        return Page();
                    }
                }
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
