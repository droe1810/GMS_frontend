using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimetableSystem.Services;
using WebClient.DTO.Request;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.khaothi
{
    public class ListApplicationModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListApplicationModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<GetRequestDTO> ListRequest { get; set; }

        public String Msg { get; set; }
        public IActionResult OnGet()
        {
            GetUserDTO user = AuthenticationHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext);

            if (user == null || !AuthenticationHelper.IsKhaoThi(user))
            {
                return Redirect("/AccessDenied");
            }

            try
            {
                ListRequest = RequestService.GetAll();
                return Page();
            }
            catch
            {
                return Redirect("/SeverError");
            }
        }

        public IActionResult OnPost(int requestId, int studentId, int gradeId, int newGrade)
        {
            GetUserDTO user = AuthenticationHelper.GetAuthenticatedUser(_httpContextAccessor.HttpContext);

            if (user == null || !AuthenticationHelper.IsKhaoThi(user))
            {
                return Redirect("/AccessDenied");
            }

            try
            {
                ResultForUpdateRequestDTO resultUpdate = RequestService.UpdateRequest(requestId, newGrade);
                if (resultUpdate != null && resultUpdate.statusName.Equals("Approved"))
                {
                    UserService.ReFund(studentId);
                }

                List<GetRequestDTO> ListAll = RequestService.GetAll();
                foreach (GetRequestDTO request in ListAll)
                {
                    if (request.RequestStatusId == 1 && request.StudentId == studentId && request.GradeId == gradeId && request.RequestId != requestId)
                    {
                        /* ResultForUpdateRequestDTO resultUpdate =*/
                        RequestService.UpdateRequest(request.RequestId, newGrade);
                    }

                }

                ListRequest = RequestService.GetAll();
                Msg = "Update success";
                return Page();
            }
            catch 
            {
                return Redirect("/SeverError");
            }
        }
    }
}
