using BloodDonationSystem.BLL.Services.BloodTypeRepoService;
using BloodDonationSystem.BLL.Services.UserService;
using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.Requests;
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

        public UpdateUserRequest UpdateUser { get; set; }

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
            Guid? bloodTypeId = null;
            if (Guid.TryParse(form["BloodTypeId"], out var parsedBloodTypeId))
            {
                bloodTypeId = parsedBloodTypeId;
            }
            if (bloodTypeId.HasValue)
            {
                var bloodType = await _bloodTypeService.GetBloodTypeByIDAsync(bloodTypeId.Value);
                if (bloodType != null)
                {
                    user.BloodTypeId = bloodType.BloodTypeId;
                    user.BloodType = bloodType;
                }

            }
            if (Enum.TryParse<UserGender>(form["Gender"], out var gender))
                user.Gender = gender;
            user.Address = form["Address"];
            user.Phone = form["Phone"];
            if (Enum.TryParse<UserRole>(form["Role"], out var role))
                user.Role = role;
            if (Enum.TryParse<UserStatus>(form["Status"], out var status))
                user.Status = status;
            user.IsDonor = form["IsDonor"] == "true";

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

            await _userService.UpdateUserAsync(UpdateUser);

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
