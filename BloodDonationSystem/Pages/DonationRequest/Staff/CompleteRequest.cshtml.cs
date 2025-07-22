using BloodDonationSystem.BLL.Services.DonationRequestService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.DonationRequest.Staff;

public class CompleteModel : PageModel
{
    private readonly IDonationRequestService _service;
    public BusinessObject.Entities.DonationRequest Request { get; set; }

    public CompleteModel(IDonationRequestService service) => _service = service;

    [BindProperty(SupportsGet = true)]
    public Guid RequestId { get; set; }

    [BindProperty]
    public int AmountBlood { get; set; }

    public async Task OnGetAsync()
    {
        Request = await _service.GetDonationRequestByIdAsync(RequestId);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _service.CompleteDonationRequestAsync(RequestId, AmountBlood);
        return RedirectToPage("ManageRequests");
    }
}