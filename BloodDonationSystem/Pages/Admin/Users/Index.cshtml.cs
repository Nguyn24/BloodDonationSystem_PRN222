using BloodDonationSystem.BLL.Services.BloodTypeRepoService;
using BloodDonationSystem.BLL.Services.UserService;
using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonationSystem.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IBloodTypeService _bloodTypeService;

        public IndexModel(IUserService userService, IBloodTypeService bloodTypeService)
        {
            _userService = userService;
            _bloodTypeService = bloodTypeService;
        }

        public IList<User> Users { get; set; } = default!;
        public IList<BloodType> BloodTypes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Users = await _userService.GetUsersAsyncHavePagination();
            BloodTypes = await _bloodTypeService.GetBloodTypeAsync();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var form = Request.Form;

            if (!Guid.TryParse(form["UserId"], out Guid userId))
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Cập nhật thông tin
            user.Name = form["FullName"];
            user.Email = form["Email"];
            user.DateOfBirth = DateTime.TryParse(form["DateOfBirth"], out var dt)
                ? DateOnly.FromDateTime(dt)
                : user.DateOfBirth;
            user.BloodTypeId = Guid.TryParse(form["BloodTypeId"], out var bloodTypeId) ? bloodTypeId : user.BloodTypeId;
            if (Enum.TryParse<UserGender>(form["Gender"], out var gender))
                user.Gender = gender;
            user.Address = form["Address"];
            user.Phone = form["Phone"];
            if (Enum.TryParse<UserRole>(form["Role"], out var role))
                user.Role = role;
            if (Enum.TryParse<UserStatus>(form["Status"], out var status))
                user.Status = status;
            user.IsDonor = form["IsDonor"] == "true";

            await _userService.UpdateUserAsync(user);

            return RedirectToPage();
        }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(id); // gọi từ service
            Users = await _userService.GetUsersAsyncHavePagination();
            BloodTypes = await _bloodTypeService.GetBloodTypeAsync();
            return Page();
        }

    }
}
