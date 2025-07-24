using BloodDonationSystem.BLL.Services.DonationRequestService;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BloodDonationSystem.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BloodDonationSystem.Pages.DonationRequest.Member;

public class MyRequestsModel : PageModel
{
    private readonly IDonationRequestService _service;
    private readonly IHubContext<NotificationHub> _hubContext;

    public List<BusinessObject.Entities.DonationRequest> Requests { get; set; } = new();


    public Dictionary<DateTime, List<BusinessObject.Entities.DonationRequest>> RequestsByDate { get; set; } = new();

    public MyRequestsModel(IDonationRequestService service, IHubContext<NotificationHub> hubContext)
    {
        _service = service;
        _hubContext = hubContext;
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
        // Lấy lại request vừa huỷ để lấy thông tin gửi notify
        var cancelledRequest = await _service.GetDonationRequestByIdAsync(requestId);
        await _hubContext.Clients.All.SendAsync("notify", new
        {
            requestId = cancelledRequest.RequestId, 
            user = new { name = cancelledRequest.User?.Name },
            bloodType = new { name = cancelledRequest.BloodType?.Name },
            requestTime = cancelledRequest.RequestTime,
            amountBlood = cancelledRequest.AmountBlood,
            status = cancelledRequest.Status.ToString()
        });
        return RedirectToPage();
    }
}
