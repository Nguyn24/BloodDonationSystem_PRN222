using Microsoft.AspNetCore.SignalR;

namespace BloodDonationSystem.SignalR.Hubs;

public class ArticlesHub : Hub
{
    // public async Task CreateArticle(NewsArticle createdArticle)
    // {
    //     await Clients.All.SendAsync("ReceiveNewArticle", createdArticle);
    // }
    //
    // public async Task UpdateArticle(NewsArticle updatedArticle)
    // {
    //     await Clients.All.SendAsync("ReceiveUpdatedArticle", updatedArticle);
    // }
    //
    // public async Task DeleteArticle(NewsArticle deletedArticle)
    // {
    //     await Clients.All.SendAsync("ReceiveDeletedArticle", deletedArticle);
    // }
}