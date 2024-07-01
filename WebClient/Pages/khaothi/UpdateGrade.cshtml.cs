using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.DTO.Grade;
using WebClient.DTO.Session;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.khaothi
{
    public class UpdateGradeModel : PageModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public int CurrentSessonId {  get; set; }
        public int CurrentGradeId {  get; set; }

        public List<GetSessionDTO> ListSession { get; set; }
        public List<GetGradeDTO> ListGrade { get; set; }

        public string Msg { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(string username, int? sessionId, int? gradeId, int? newGradeValue)
        {
            GetUserDTO u = UserService.GetStudent(username);

            if(u.Username == null)
            {
                Username = username;
                Msg = "No Student Found";
                return Page();
            }

            Username = u.Username;
            ListSession = SessionService.GetSessionByStudent(u.Id);

            if (sessionId == null)
            {
                ListGrade = GradeService.GetGradesBySessionGradedByTeacher(ListSession[0].Id);
            }
            else
            {
                ListGrade = GradeService.GetGradesBySessionGradedByTeacher((int)sessionId);
                CurrentSessonId = (int)sessionId;

                if(gradeId != null)
                {
                    CurrentGradeId = (int)gradeId;
                    if(newGradeValue != null)
                    {
                        bool updateSuccess = StudentGradeService.UpdateGradeForStudent((int)gradeId, u.Id, (decimal)newGradeValue);

                        if (updateSuccess)
                        {
                            Msg = "Update success";

                        }
                        else
                        {
                            Msg = "Update fail";
                        }
                    }
                }
            }
            return Page();
        }
    }
}
