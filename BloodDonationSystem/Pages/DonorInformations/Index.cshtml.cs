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
    public class IndexModel : PageModel
    {   private readonly IDonorInfomationService _donorInformationService;
        public IndexModel(IDonorInfomationService donorInfomationService)
        {
            _donorInformationService = donorInfomationService;
        }

        public IList<DonorInformation> DonorInformation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            DonorInformation = await _donorInformationService.GetDonorInfoAsync();
        }
    }
}
