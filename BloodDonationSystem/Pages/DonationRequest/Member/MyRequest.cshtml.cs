using BloodDonationSystem.BLL.Services.DonationRequestService;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.DonationRequest.Member;

public class MyRequestsModel : PageModel
{
    private readonly IDonationRequestService _service;

    public List<BusinessObject.Entities.DonationRequest> Requests { get; set; } = new();


    public Dictionary<DateTime, List<BusinessObject.Entities.DonationRequest>> RequestsByDate { get; set; } = new();

    public MyRequestsModel(IDonationRequestService service)
    {
        _service = service;
    }

    public async Task OnGetAsync()
    {
        Requests = await _service.GetMyDonationRequestsAsync();

        RequestsByDate = Requests
            .GroupBy(r => r.RequestTime.Date)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public async Task<IActionResult> OnPostCancelAsync(Guid requestId)
    {
        await _service.DeleteDonationRequestAsync(requestId);
        return RedirectToPage();
    }
}
