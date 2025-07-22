using BloodDonationSystem.BLL.Services.DonationRequestService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.DonationRequest.Staff;

public class ManageRequestsModel : PageModel
{
    private readonly IDonationRequestService _service;
    public List<BusinessObject.Entities.DonationRequest> Requests { get; set; }

    public ManageRequestsModel(IDonationRequestService service) => _service = service;

    public async Task OnGetAsync() => Requests = await _service.GetDonationRequestAsync();

    public async Task<IActionResult> OnPostConfirmAsync(Guid requestId)
    {
        await _service.ConfirmDonationRequestAsync(requestId);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostFailAsync(Guid requestId, string reason)
    {
        await _service.UpdateFailedDonationRequestAsync(requestId, reason);
        return RedirectToPage();
    }
}