using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;

namespace BloodDonationSystem.Pages.BloodStoredPage
{
    public class DetailsModel : PageModel
    {
        private readonly BloodDonationSystem.DAL.DBContext.BloodDonationPrn222Context _context;

        public DetailsModel(BloodDonationSystem.DAL.DBContext.BloodDonationPrn222Context context)
        {
            _context = context;
        }

        public BloodStored BloodStored { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodstored = await _context.BloodStoreds.FirstOrDefaultAsync(m => m.StoredId == id);
            if (bloodstored == null)
            {
                return NotFound();
            }
            else
            {
                BloodStored = bloodstored;
            }
            return Page();
        }
    }
}
