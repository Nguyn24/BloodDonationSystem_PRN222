using BloodDonationSystem.DAL.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonationSystem.DAL.Shared;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using BloodDonationPrn222Context dbContext =
            scope.ServiceProvider.GetRequiredService<BloodDonationPrn222Context>();

        dbContext.Database.Migrate();
    }
}

