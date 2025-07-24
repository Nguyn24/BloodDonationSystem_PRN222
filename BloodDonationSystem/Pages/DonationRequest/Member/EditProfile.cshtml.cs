using BloodDonationSystem.BLL.Services.UserService;
using BloodDonationSystem.DAL.Repositories.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.DonationRequest.Member;

public class EditProfileModel : PageModel
{
    private readonly IUserService _userService;

    public EditProfileModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public UpdateUserRequest UpdateUser { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var userIdStr = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
        {
            return RedirectToPage("/Login");
        }

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        UpdateUser = new UpdateUserRequest
        {
            UserId = user.UserId,
            FullName = user.Name,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            Address = user.Address,
            Phone = user.Phone,
            Role = user.Role,
            Status = user.Status,
            IsDonor = user.IsDonor,
            BloodType = user.BloodType?.Name
        };

        return Page();
    }

    // public async Task<IActionResult> OnPostAsync()
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return Page();
    //     }
    //
    //     await _userService.UpdateUserAsync(UpdateUser);
    //     return RedirectToPage("/DonationRequest/Member/EditProfile"); // hoặc trang nào bạn muốn
    // }
}