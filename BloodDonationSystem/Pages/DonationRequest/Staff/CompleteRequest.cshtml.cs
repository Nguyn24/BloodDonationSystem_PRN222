using BloodDonationSystem.BLL.Services.DonationRequestService;
using BloodDonationSystem.SignalR.Hubs;
using BusinessObject.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace BloodDonationSystem.Pages.DonationRequest.Staff;

public class CompleteModel : PageModel
{
    private readonly IDonationRequestService _service;
    private readonly IHubContext<NotificationHub> _hubContext;
    public BusinessObject.Entities.DonationRequest Request { get; set; }
    
    public CompleteModel(IDonationRequestService service, IHubContext<NotificationHub> hubContext)
    {
        _service = service;
        _hubContext = hubContext;
    }

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
        var donationRequest = await _service.CompleteDonationRequestAsync(RequestId, AmountBlood);
        await _hubContext.Clients.User(donationRequest.UserId.ToString())
            .SendAsync("ReceiveNotification", new
            {
                message = "Your donation has been completed successfully.",
                status = donationRequest.Status.ToString()
            });
        return RedirectToPage();
    }
}
