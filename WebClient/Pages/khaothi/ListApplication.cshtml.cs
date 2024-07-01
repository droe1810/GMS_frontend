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
    }
}
