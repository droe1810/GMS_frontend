using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.DTO.Request;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.khaothi
{
    public class ListApplicationModel : PageModel
    {
        public List<GetRequestDTO> ListRequest {  get; set; }

        public String Msg { get; set; }
        public IActionResult OnGet()
        {
            ListRequest = RequestService.GetAll();
            return Page();
        }

        public IActionResult OnPost(int requestId, int studentId, int gradeId, int newGrade)
        {
            ResultForUpdateRequestDTO resultUpdate = RequestService.UpdateRequest(requestId, newGrade);
            if (resultUpdate != null && resultUpdate.statusName.Equals("Approved"))
            {
                UserService.ReFund(studentId);
            }

            List<GetRequestDTO> ListAll = RequestService.GetAll();
            foreach (GetRequestDTO request in ListAll) {
                if(request.RequestStatusId == 1 && request.StudentId == studentId && request.GradeId == gradeId && request.RequestId != requestId)
                {
                   /* ResultForUpdateRequestDTO resultUpdate =*/  
                    RequestService.UpdateRequest(request.RequestId, newGrade);
                }
                
            }

            ListRequest = RequestService.GetAll();
            Msg = "Update success";
            return Page();
        }
    }
}
