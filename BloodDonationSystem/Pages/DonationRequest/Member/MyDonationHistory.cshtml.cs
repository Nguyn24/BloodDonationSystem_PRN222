using BloodDonationSystem.BLL.Services.DonationHistoryService;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.DonationRequest.Member;

public class MyDonationHistoryModel : PageModel
{
    private readonly IDonationHistoryService _service;
    public List<DonationsHistory> History { get; set; }

    public MyDonationHistoryModel(IDonationHistoryService service) => _service = service;

    public async Task OnGetAsync() => History = await _service.GetMyDonationHistoryAsync();
}