using BloodDonationSystem.BLL.Services.DonationRequestService;
using BloodDonationSystem.SignalR.Hubs;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
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
    public List<string> BloodTypes { get; set; } = new();

    public int PendingCount { get; set; }
    public int ProcessedCount { get; set; }

    public string ActiveTab { get; set; } = "Pending";

    public FilterModel Filter { get; set; } = new();
    
    public async Task OnGetAsync(string? tab, string? bloodType, string? componentType, DateTime? date)
    {
        ActiveTab = tab ?? "Pending";
        Filter = new FilterModel
        {
            BloodType = bloodType,
            ComponentType = componentType,
            Date = date
        };

        var allRequests = await _service.GetDonationRequestAsync();

        // Gán danh sách để render bộ lọc
        BloodTypes = allRequests.Select(r => r.BloodType?.Name).Where(n => n != null).Distinct().ToList()!;
       

        // Lọc theo Filter
        IEnumerable<BusinessObject.Entities.DonationRequest> filtered = allRequests;

        if (!string.IsNullOrEmpty(Filter.BloodType))
            filtered = filtered.Where(r => r.BloodType?.Name == Filter.BloodType);

        

        if (Filter.Date.HasValue)
            filtered = filtered.Where(r => r.RequestTime.Date == Filter.Date.Value.Date);

        // Đếm
        PendingCount = allRequests.Count(r => r.Status == DonationRequestStatus.Pending);
        ProcessedCount = allRequests.Count(r => r.Status != DonationRequestStatus.Pending);

        // Tab filter
        Requests = filtered
            .Where(r => ActiveTab == "Pending"
                ? r.Status == DonationRequestStatus.Pending
                : r.Status != DonationRequestStatus.Pending)
            .OrderByDescending(r => r.RequestTime)
            .ToList();
    }

    public async Task<IActionResult> OnPostConfirmAsync(Guid requestId)
    {
        await _service.ConfirmDonationRequestAsync(requestId);
        await _hubContext.Clients.All.SendAsync("notifystatus", "Your donation request has been updated.");
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostFailAsync(Guid requestId, string reason)
    {
        await _service.UpdateFailedDonationRequestAsync(requestId, reason);
        await _hubContext.Clients.All.SendAsync("notifystatus", "Your donation request has been updated.");
        return RedirectToPage();
    }
    
    public class FilterModel
    {
        public string? BloodType { get; set; }
        public string? ComponentType { get; set; }
        public DateTime? Date { get; set; }
    }
}
