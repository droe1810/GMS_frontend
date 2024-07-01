using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.DTO.Grade;
using WebClient.Services;

namespace WebClient.Pages.khaothi
{
    public class IndexModel : PageModel
    {
        public List<GetGradeDTO> ListGrade { get; set; }
        public IActionResult OnGet()
        {
            ListGrade = GradeService.GetGradesGradedByKhaoThi();
            return Page();
        }

        //public IActionResult OnGet()
        //{
        //    return Page();
        //}
    }
}
