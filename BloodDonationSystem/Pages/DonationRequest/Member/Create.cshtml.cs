using BloodDonationSystem.BLL.Services.DonationRequestService;
using BloodDonationSystem.DAL.Repositories.Requests;
using BloodDonationSystem.SignalR.Hubs;
using BusinessObject.Entities;
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

        var donationRequest = await _service.CreateDonationRequestAsync(Request);
        await _hubContext.Clients.All.SendAsync("notify", new
        {
            requestId = donationRequest.RequestId,
            user = new { name = donationRequest.User?.Name },
            bloodType = new { name = donationRequest.BloodType?.Name },
            requestTime = donationRequest.RequestTime,
            amountBlood = donationRequest.AmountBlood,
            status = donationRequest.Status.ToString()
        });
        return RedirectToPage("/DonationRequest/Member/MyRequest");
    }
}
