using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;
using BloodDonationSystem.BLL.Services.BloodTypeRepoService;

namespace BloodDonationSystem.Pages.BloodTypes
{
    public class IndexModel : PageModel
    {
        private readonly IBloodTypeService _service;

        public IndexModel(IBloodTypeService service)
        {
            _service = service;
        }

        public IList<BloodType> BloodType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            BloodType = await _service.GetBloodTypeAsync();
        }
    }
}
