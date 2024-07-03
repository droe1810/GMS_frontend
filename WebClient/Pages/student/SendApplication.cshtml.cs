using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebClient.DTO.Grade;
using WebClient.DTO.Request;
using WebClient.DTO.Session;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.student
{
    public class SendApplicationModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserDTO Student { get; set; }
        public List<GetGradeDTO> ListGrade { get; set; }
        public List<GetSessionDTO> ListSession { get; set; }
        public int CurrentGradeId { get; set; }
        public int CurrentSessonId { get; set; }

        public string Msg {  get; set; }

        public SendApplicationModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet(int? sessionId, int? gradeId, string? content, string isSubmit)
        {
            string userJson = _httpContextAccessor.HttpContext.Session.GetString("currentUser");
            GetUserDTO u = JsonSerializer.Deserialize<GetUserDTO>(userJson);
            Student = u;
            ListSession = SessionService.GetSessionByStudent(u.Id);
            if (sessionId == null)
            {
                ListGrade = GradeService.GetGradesBySessionGradedByKhaoThi(ListSession[0].Id);
            }
            else
            {
                ListGrade = GradeService.GetGradesBySessionGradedByKhaoThi((int)sessionId);
                CurrentSessonId = (int)sessionId;

                if (gradeId != null)
                {
                    CurrentGradeId = (int)gradeId;

                    if (!string.IsNullOrEmpty(isSubmit))
                    {
                        ResultForCreateRequestDTO result = RequestService.CreateRequest(u.Id, (int)gradeId, content);
                        if(result.IsSuccess == true)
                        {
                            Msg = "Send application success";
                            AccountBalanceDTO  deductFunds = UserService.DeductFunds(u.Id);
                            u.AccountBalance = deductFunds.NewAccountBalance;

                            Student = u;

                            userJson = JsonSerializer.Serialize(u);
                            _httpContextAccessor.HttpContext.Session.SetString("currentUser", userJson);
                        }
                        else
                        {
                            Msg = "Send Application fail, " + result.msg;
                        }
                    }
                }
            }
            return Page();

        }
    }
}
