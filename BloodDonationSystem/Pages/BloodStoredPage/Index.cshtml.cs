using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;
using BloodDonationSystem.BLL.Services.BloodStoredService;

namespace BloodDonationSystem.Pages.BloodStoredPage
{
    public class IndexModel : PageModel
    {
        private readonly IBloodStoredService _service;

        public IndexModel(IBloodStoredService service)
        {
            _service = service;
        }

        public IList<BloodStored> BloodStored { get;set; } = default!;

        public async Task OnGetAsync()
        {
            BloodStored = await _service.GetBloodStoredTypes();
        }
    }
}
