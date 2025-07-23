using BloodDonationSystem.BLL.Services.DonationRequestService;
using BusinessObject.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.DonationRequest.Staff;

public class CompleteModel : PageModel
{
    private readonly IDonationRequestService _service;

    public CompleteModel(IDonationRequestService service) => _service = service;

    [BindProperty]
    public Guid RequestId { get; set; }

    [BindProperty]
    public int AmountBlood { get; set; }

    public List<BusinessObject.Entities.DonationRequest> ScheduledRequests { get; set; }

    public async Task OnGetAsync()
    {
        ScheduledRequests = await _service.GetRequestsByStatusAsync(DonationRequestStatus.Scheduled);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _service.CompleteDonationRequestAsync(RequestId, AmountBlood);
        return RedirectToPage(); // reload lại trang
    }
}
