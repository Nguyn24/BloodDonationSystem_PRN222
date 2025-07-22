using BloodDonationSystem.BLL.Services.DonationRequestService;
using BloodDonationSystem.DAL.Repositories.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloodDonationSystem.Pages.DonationRequest.Member;

// CreateDonationRequest.cshtml.cs
public class CreateDonationRequestModel : PageModel
{
    private readonly IDonationRequestService _service;

    public CreateDonationRequestModel(IDonationRequestService service) => _service = service;

    [BindProperty]
    public CreateDonationRequest Request { get; set; }

    public void OnGet()
    {
        Request = new CreateDonationRequest
        {
            Date = DateTime.Today
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        await _service.CreateDonationRequestAsync(Request);
        return RedirectToPage("/DonationRequest/Member/MyRequest");
    }
}
