﻿using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages
{
    public class LoginModel : PageModel
    {
      

        // [BindProperty] public string Email { get; set; }
        // [BindProperty] public string Password { get; set; }
        //
        // public string ErrorMessage { get; set; }
        //
        // public async Task<IActionResult> OnPostAsync()
        // {
        //     // ✅ Nếu là logout
        //     if (Request.Form.ContainsKey("logout"))
        //     {
        //         HttpContext.Session.Clear();
        //         return Page(); // quay lại login rỗng
        //     }
        //
        //     // ✅ Kiểm tra login input
        //     if (string.IsNullOrWhiteSpace(Email))
        //     {
        //         ErrorMessage = "Email không được để trống.";
        //         return Page();
        //     }
        //
        //     if (string.IsNullOrWhiteSpace(Password))
        //     {
        //         ErrorMessage = "Mật khẩu không được để trống.";
        //         return Page();
        //     }
        //
        //     var user = await _accountService.AuthenticateAsync(Email, Password);
        //
        //     if (user == null)
        //     {
        //         ErrorMessage = "Email hoặc mật khẩu không đúng.";
        //         return Page();
        //     }
        //
        //     HttpContext.Session.SetString("Role", user.AccountRole?.ToString() ?? "0");
        //     HttpContext.Session.SetInt32("UserId", user.AccountId);
        //     HttpContext.Session.SetString("Email", user.AccountEmail);
        //     HttpContext.Session.SetString("AccountName", user.AccountName);
        //     
        //     if (user.AccountRole == 0)
        //     {
        //         return RedirectToPage("/SystemAccounts");
        //     }
        //
        //     return RedirectToPage("/NewsArticles");
        // }
        //
        // public IActionResult OnPostLogout()
        // {
        //     HttpContext.Session.Clear();
        //     return RedirectToPage("/NewsArticles");
        // }
    }
}
