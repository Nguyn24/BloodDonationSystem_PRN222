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
using BloodDonationSystem.BLL.Services.BloodStoredService;
using BloodDonationSystem.BLL.Services.BloodTypeRepoService;
using BloodDonationSystem.DAL.Repositories.Requests;

namespace BloodDonationSystem.Pages.BloodStoredPage
{
    public class EditModel : PageModel
    {
        private readonly IBloodStoredService _service;
        private readonly IBloodTypeService _type;

        public EditModel(IBloodStoredService service, IBloodTypeService type)
        {
            _service = service;
            _type = type;
        }

        [BindProperty]
        public UpdateBloodStoredRequest UpdateRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodstored = await _service.GetBloodStoredTypeById(id.Value);
            if (bloodstored == null)
            {
                return NotFound();
            }

            UpdateRequest = new UpdateBloodStoredRequest
            {
                StoredId = bloodstored.StoredId,
                BloodTypeId = bloodstored.BloodTypeId,
                Quantity = bloodstored.Quantity
            };

            ViewData["BloodTypeId"] = new SelectList(await _type.GetBloodTypeAsync(),"BloodTypeId" ,"Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _service.UpdateBloodStoredType(UpdateRequest);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BloodStoredExists(UpdateRequest.StoredId))
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

        private async Task<bool> BloodStoredExists(Guid id)
        {
            var bloodStored = await _service.GetBloodStoredTypeById(id);
            return bloodStored != null && bloodStored.StoredId == id;
        }
    }
}
