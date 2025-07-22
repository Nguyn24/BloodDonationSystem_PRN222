using BloodDonationSystem.BLL.Services.UserService;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystem.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        public DateOnly  DateOfBirth { get; set; }

        [BindProperty]
        [Required]
        public UserGender Gender { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Vui lòng nh?p ??y ?? thông tin.";
                return Page();
            }

            var request = new RegisterRequest
            {
                Name = Name,
                Email = Email,
                Password = Password,
                DateOfBirth = DateOfBirth,
                Gender = Gender,
                Address = Address,
                Phone = Phone
            };

            try
            {
                await _userService.Register(request); // <- OK
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                ErrorMessage = "??ng ký th?t b?i: " + ex.Message;
                return Page();
            }
        }
    }
}
