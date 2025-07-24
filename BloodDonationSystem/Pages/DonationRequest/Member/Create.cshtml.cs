using BloodDonationSystem.BLL.Services.DonationRequestService;
using BloodDonationSystem.DAL.Repositories.Requests;
using BloodDonationSystem.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace BloodDonationSystem.Pages.DonationRequest.Member;

// CreateDonationRequest.cshtml.cs
public class CreateDonationRequestModel : PageModel
{
    private readonly IDonationRequestService _service;
    private readonly IHubContext<NotificationHub> _hubContext; // < -->
    
    public CreateDonationRequestModel(IDonationRequestService service, IHubContext<NotificationHub> hubContext)
    {
        _service = service;
        _hubContext = hubContext;
    }

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
        await _hubContext.Clients.All.SendAsync("notify", "New donation request has been created.");
        return RedirectToPage("/DonationRequest/Member/MyRequest");
    }
}
