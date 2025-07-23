using BloodDonationSystem.BLL.Services.DonationRequestService;
using BloodDonationSystem.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace BloodDonationSystem.Pages.DonationRequest.Staff;

public class ManageRequestsModel : PageModel
{
    private readonly IDonationRequestService _service;
    private readonly IHubContext<NotificationHub> _hubContext;
    public List<BusinessObject.Entities.DonationRequest> Requests { get; set; }
    public ManageRequestsModel(IDonationRequestService service, IHubContext<NotificationHub> hubContext)
    {
        _service = service;
        _hubContext = hubContext;
    }

    public async Task OnGetAsync() => Requests = await _service.GetDonationRequestAsync();

    public async Task<IActionResult> OnPostConfirmAsync(Guid requestId)
    {
        var donationRequest = await _service.ConfirmDonationRequestAsync(requestId);
        await _hubContext.Clients.User(donationRequest.UserId.ToString())
            .SendAsync("ReceiveNotification", new
            {
                message = "Your donation request has been confirmed and scheduled.",
                status = donationRequest.Status.ToString()
            });
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostFailAsync(Guid requestId, string reason)
    {
        var donationRequest = await _service.UpdateFailedDonationRequestAsync(requestId, reason);
        await _hubContext.Clients.User(donationRequest.UserId.ToString())
            .SendAsync("ReceiveNotification", new
            {
                message = "Your donation request failed. Reason: " + reason,
                status = donationRequest.Status.ToString()
            });
        return RedirectToPage();
    }
}