
using BloodDonationSystem.BLL.Services.UserService;
using BloodDonationSystem.DAL.Repositories.Responses;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.DonationRequest.Member
{
    public class ProfileModel : PageModel
    {
        private readonly IUserService _userService;

        public ProfileModel(IUserService userService)
        {
            _userService = userService;
        }

        public GetCurrentUserResponse User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            User = await _userService.GetCurrentUserAsync();
            if (User == null)
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }
    }
}