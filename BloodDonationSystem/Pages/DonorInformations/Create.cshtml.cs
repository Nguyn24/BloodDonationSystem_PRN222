using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;
using BloodDonationSystem.BLL.Services.DonorInfomationService;
using BloodDonationSystem.BLL.Services.UserService;
using BloodDonationSystem.DAL.Repositories.Requests;

namespace BloodDonationSystem.Pages.DonorInformations
{
    public class CreateModel : PageModel
    {
        private readonly IDonorInfomationService _donorInformationService;
        private readonly IUserService _userService;

        public CreateModel(IDonorInfomationService donorInfomationService, IUserService service)
        {
            _donorInformationService = donorInfomationService;
            _userService = service;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["UserId"] = new SelectList(await _userService.GetUsersAsync(), "UserId", "Name");
            return Page();
        }

        [BindProperty]
        public CreateDonorRequest CreateRequest { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["UserId"] = new SelectList(await _userService.GetUsersAsync(), "UserId", "Name");
                return Page();
            }

            await _donorInformationService.CreateDonorAsync(CreateRequest);

            return RedirectToPage("./Index");
        }
    }
}
