using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebClient.DTO.SessionStudent;
using WebClient.DTO.StudentGrade;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.student
{
    public class ViewGradeModel : PageModel
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ViewGradeModel()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }
        public StudentViewGradeDTO ViewGrade { get; set; }
        public int SessionId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }

        public decimal Avg { get; set; }

        public GetStatusDTO Status { get; set; }


        public IActionResult OnGet(int sessionId, int courseId, string courseName, string className, string teacherName)
        {
            string userJson = _httpContextAccessor.HttpContext.Session.GetString("currentUser");

            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Login");
            }
            GetUserDTO u = JsonSerializer.Deserialize<GetUserDTO>(userJson);

            u = UserService.GetStudent(u.Username);

            userJson = JsonSerializer.Serialize(u);
            _httpContextAccessor.HttpContext.Session.SetString("currentUser", userJson);

            SessionId = sessionId;
            CourseId = courseId;
            CourseName = courseName;
            ClassName = className;
            TeacherName = teacherName;

            ViewGrade = StudentGradeService.StudentViewGrade(u.Id, courseId);
            Avg = SessionStudentService.GetAvgGrade(courseId, u.Id);
            Status = SessionStudentService.GetStatus(courseId, u.Id);


            return Page();
        }
    }
}
