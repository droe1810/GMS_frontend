using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebClient.DTO.User;
using WebClient.Services;

namespace WebClient.Pages.student
{
    public class RechargeModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RechargeModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet(int amount)
        {
            string userJson = _httpContextAccessor.HttpContext.Session.GetString("currentUser");


            GetUserDTO u = JsonSerializer.Deserialize<GetUserDTO>(userJson);

            AccountBalanceDTO recharge = UserService.Recharge(u.Id, amount);
            u.AccountBalance = recharge.NewAccountBalance;

            userJson = JsonSerializer.Serialize(u);
            _httpContextAccessor.HttpContext.Session.SetString("currentUser", userJson);
            return RedirectToPage("/student/Index");
        }
    }
}
