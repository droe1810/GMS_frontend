using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.DTO.Request;
using WebClient.Services;

namespace WebClient.Pages.khaothi
{
    public class ListApplicationModel : PageModel
    {
        public List<GetRequestDTO> ListRequest {  get; set; }
        public IActionResult OnGet()
        {
            ListRequest = RequestService.GetAll();
            return Page();
        }

        public IActionResult OnPost(int studentId, int gradeId, int newGrade)
        {
            List<GetRequestDTO> ListAll = RequestService.GetAll();
            foreach (GetRequestDTO request in ListAll) {
                if(request.RequestStatusId == 1 && request.StudentId == studentId && request.GradeId == gradeId)
                {
                    RequestService.UpdateRequest(request.RequestId, newGrade);
                }
                
            }

            ListRequest = RequestService.GetAll();
            return Page();
        }
    }
}
