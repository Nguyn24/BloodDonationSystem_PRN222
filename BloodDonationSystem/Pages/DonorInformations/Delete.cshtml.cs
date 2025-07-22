using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;
using BloodDonationSystem.BLL.Services.DonorInfomationService;

namespace BloodDonationSystem.Pages.DonorInformations
{
    public class DeleteModel : PageModel
    {
        private readonly IDonorInfomationService _donorInformationService;

        public DeleteModel(IDonorInfomationService donorInfomationService)
        {
           _donorInformationService = donorInfomationService;
        }

        [BindProperty]
        public DonorInformation DonorInformation { get; set; } = default!;

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
            else
            {
                DonorInformation = donorinformation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donorinformation = await _donorInformationService.GetById(id.Value);
            if (donorinformation != null)
            {
                DonorInformation = donorinformation;
                _donorInformationService.DeleteDonorAsync(donorinformation);
            }

            return RedirectToPage("./Index");
        }
    }
}
