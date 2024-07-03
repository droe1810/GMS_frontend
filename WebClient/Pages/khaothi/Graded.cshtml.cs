using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.khaothi
{
    public class GradedModel : PageModel
    {
        public List<GetUserDTO> ListStudent { get; set; }

        public string[][] GradeTable { get; set; }
        public string CourseName { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Msg { get; set; }
        public IActionResult OnGet(int courseId, string courseName, int gradeId, string gradeName)
        {
            GetData(courseId, courseName, gradeId, gradeName);

            return Page();
        }

        public IActionResult OnPost(List<int> studentIds, List<string> grades, int courseId, string courseName, int gradeId, string gradeName)
        {
          

            for (int i = 0; i < studentIds.Count; i++)
            {
                if (!string.IsNullOrEmpty(grades[i]))
                {
                    decimal gradeValue = decimal.Parse(grades[i]);

                    bool exist = StudentGradeService.CheckStudentGradeExist(gradeId, studentIds[i]);
                    if (!exist)
                    {
                       
                        bool success = StudentGradeService.GradedForStudent(gradeId, studentIds[i], gradeValue);
                        if (!success)
                        {
                            Msg += $"Grade Fail at {studentIds[i]} ";
                        }
                    }
                    else
                    {
                        bool updateSuccess = StudentGradeService.UpdateGradeForStudent(gradeId, studentIds[i], gradeValue);
                        if (!updateSuccess)
                        {
                            Msg += $"Grade Fail at {studentIds[i]} ";
                        }
                    }
                }
                else
                {
                    bool exist = StudentGradeService.CheckStudentGradeExist(gradeId, studentIds[i]);
                    if (exist)
                    {
                        bool deleteSuccess = StudentGradeService.DeleteStudentGrade(gradeId, studentIds[i]);
                        if (!deleteSuccess)
                        {
                            Msg += $"Grade Fail at {studentIds[i]} ";
                        }
                    }

                }
            }
            if (string.IsNullOrEmpty(Msg))
            {
                Msg = "Graded success";
            }

            GetData(courseId, courseName, gradeId, gradeName);
            return Page();
        }

        public void GetData(int courseId, string courseName, int gradeId, string gradeName)
        {
            ListStudent = UserService.GetStudentInCourse(courseId);
            CourseName = courseName;
            GradeName = gradeName;
            GradeId = gradeId;

            GradeTable = new string[ListStudent.Count][];
            for (int i = 0; i < ListStudent.Count; i++)
            {
                GradeTable[i] = new string[1];
            }

            for (int i = 0; i < ListStudent.Count; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    GradeTable[i][j] = StudentGradeService.GetGradeForStudentByGradeId(gradeId, ListStudent[i].Id).GradeValue;
                }
            }
        }
    }
}