using BloodDonationSystem.BLL.Services.DonorInfomationService;
using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonationSystem.Pages.DonorInformations
{
    public class DetailsModel : PageModel
    {
        private readonly IDonorInfomationService _donorInformationService;

        public DetailsModel(IDonorInfomationService donorInfomationService)
        {
            _donorInformationService = donorInfomationService;
        }

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
    }
}
