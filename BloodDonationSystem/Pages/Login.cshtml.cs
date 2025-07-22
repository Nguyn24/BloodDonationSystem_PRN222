using BloodDonationSystem.BLL.Services.UserService;
using BusinessObject.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public string ErrorMessage { get; set; }
        private readonly IUserService _accountService;

        public LoginModel(IUserService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Request.Form.ContainsKey("logout"))
            {
                HttpContext.Session.Clear();
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Email can not be empty.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Password can not be empty.";
                return Page();
            }

            var user = await _accountService.AuthenticateAsync(Email, Password);

            if (user == null)
            {
                ErrorMessage = "Email or password is incorrect.";
                return Page();
            }

            HttpContext.Session.SetString("Role", user.Role.ToString());
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("AccountName", user.Name);

            if (user.Role == UserRole.Admin)
            {
                return RedirectToPage("/Admin");
            }

            return RedirectToPage("/HomePage");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/HomePage");
        }
    }
}
