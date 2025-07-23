using BloodDonationSystem.BLL.Services.BloodStoredService;
using BloodDonationSystem.BLL.Services.DonationHistoryService;
using BloodDonationSystem.BLL.Services.DonationRequestService;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.Admin.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IDonationRequestService _donationRequestService;
        private readonly IDonationHistoryService _donationHistoryService;
        private readonly IBloodStoredService _bloodStoredService;

        public List<StatusCountDto> RequestByStatus { get; set; } = new();
        public int RequestCount { get; set; } = 1;
        public List<MonthlyDonationDto> MonthlyDonations { get; set; } = new();
        public int HistoryCount { get; set; }
        public int BloodStoredCount { get; set; }

        public IndexModel(
            IDonationRequestService donationRequestService,
            IDonationHistoryService donationHistoryService,
            IBloodStoredService bloodStoredService)
        {
            _donationRequestService = donationRequestService;
            _donationHistoryService = donationHistoryService;
            _bloodStoredService = bloodStoredService;
        }

        public async Task OnGetAsync()
        {
            var request = await _donationRequestService.GetDonationRequestsByStatusAsync();
            RequestCount = request.Count;
            RequestByStatus = request.Group;
            var history = await _donationHistoryService.GetDonationHistoryByMonthAsync();
            HistoryCount = history.TotalCount;
            MonthlyDonations = history.MonthlyDonation;
            BloodStoredCount = (await _bloodStoredService.GetBloodStoredTypes()).Count;
        }
    }
}
