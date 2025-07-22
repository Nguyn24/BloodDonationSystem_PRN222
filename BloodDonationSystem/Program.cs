using BloodDonationSystem.BLL.Services.BloodStoredService;
using BloodDonationSystem.BLL.Services.BloodTypeRepoService;
using BloodDonationSystem.BLL.Services.DonationHistoryService;
using BloodDonationSystem.BLL.Services.DonationRequestService;
using BloodDonationSystem.BLL.Services.DonorInfomationService;
using BloodDonationSystem.BLL.Services.UserService;
using BloodDonationSystem.DAL;
using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.BloodStoredRepo;
using BloodDonationSystem.DAL.Repositories.BloodTypeRepo;
using BloodDonationSystem.DAL.Repositories.DonationHistoryRepo;
using BloodDonationSystem.DAL.Repositories.DonationRequestRepo;
using BloodDonationSystem.DAL.Repositories.DonorInformationRepo;
using BloodDonationSystem.DAL.Repositories.UserRepo;
using BloodDonationSystem.DAL.Shared;
using BloodDonationSystem.SignalR.Hubs;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using DonationRequestRepo = BloodDonationSystem.DAL.Repositories.DonationRequestRepo.DonationRequestRepo;

namespace BloodDonationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // // 🔧 Register DB Context
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(">>> Connection string from config: " + connectionString); // thêm dòng này
            builder.Services.AddDbContext<BloodDonationPrn222Context>(options =>
                options.UseSqlServer(connectionString));
            //
            // // 🔧 Register repositories
            builder.Services.AddScoped<IBloodStoredRepo, BloodStoredRepo>();
            builder.Services.AddScoped<IBloodTypeRepo, BloodTypeRepo>();
            builder.Services.AddScoped<IDonationHistoryRepo, DonationHistoryRepo>();
            builder.Services.AddScoped<IDonationRequestRepo, DonationRequestRepo>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IDonorInformationRepo, DonorInformationRepo>();
            
            //
            // // 🔧 Register services
            builder.Services.AddScoped<IBloodStoredService, BloodStoredService>();
            builder.Services.AddScoped<IBloodTypeService, BloodTypeService>();
            builder.Services.AddScoped<IDonationHistoryService, DonationHistoryService>();
            builder.Services.AddScoped<IDonationRequestService, DonationRequestService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDonorInfomationService, DonorInfomationService>();
            //
            // // ✅ Register FluentValidation
            // builder.Services.AddValidatorsFromAssemblyContaining<SystemAccountValidator>();
            
            builder.Services.AddScoped<UserContext>();
            builder.Services.AddScoped<PasswordHasher>();
            
            // ✅ Register Session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login"; 
                    options.AccessDeniedPath = "/AccessDenied"; 
                });
            
            // ✅ Razor Pages with default route override
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/Login", "");
            });
            
            builder.Services.AddSignalR();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.ApplyMigrations();
            app.MapRazorPages();
            app.MapHub<NotificationHub>("/notificationhub");

            app.Run();
        }
    }
}
