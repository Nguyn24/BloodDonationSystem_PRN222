using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;
using BloodDonationSystem.BLL.Services.DonorInfomationService;
using BloodDonationSystem.BLL.Services.UserService;
using BloodDonationSystem.DAL.Repositories.Requests;

namespace BloodDonationSystem.Pages.DonorInformations
{
    public class EditModel : PageModel
    {
        private readonly IDonorInfomationService _donorInformationService;
        private readonly IUserService _userService;

        public EditModel(IDonorInfomationService donorInformationService, IUserService userService)
        {
            _donorInformationService = donorInformationService;
            _userService = userService;
        }

        [BindProperty]
        public UpdateDonorRequest UpdateRequest { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donorinformation = await _donorInformationService.GetById(id.Value);
            if (donorinformation == null)
            {
                return NotFound();
            }

            UpdateRequest = new UpdateDonorRequest
            {
                DonorInfoId = donorinformation.DonorInfoId,
                Weight = donorinformation.Weight,
                Height = donorinformation.Height,
                MedicalStatus = donorinformation.MedicalStatus,
                LastChecked = donorinformation.LastChecked.DateTime
            };

            //ViewData["UserId"] = new SelectList(await _userService.GetUsersAsync(), "UserId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
            //    ViewData["UserId"] = new SelectList(await _userService.GetUsersAsync(), "UserId", "Name");
                return Page();
            }

            try
            {
                await _donorInformationService.UpdateDonorAsync(UpdateRequest);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DonorInformationExists(UpdateRequest.DonorInfoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> DonorInformationExists(Guid id)
        {
            var donor = await _donorInformationService.GetById(id);
            return donor != null && donor.DonorInfoId == id;
        }
    }
}
