using BloodDonationSystem.BLL.Services.DonationRequestService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.DonationRequest.Member;

public class MyRequestsModel : PageModel
{
    private readonly IDonationRequestService _service;
    public List<BusinessObject.Entities.DonationRequest> Requests { get; set; }

    public MyRequestsModel(IDonationRequestService service) => _service = service;

    public async Task OnGetAsync() => Requests = await _service.GetMyDonationRequestsAsync();

    public async Task<IActionResult> OnPostCancelAsync(Guid requestId)
    {
        await _service.DeleteDonationRequestAsync(requestId);
        return RedirectToPage();
    }
}