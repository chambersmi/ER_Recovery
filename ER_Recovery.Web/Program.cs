using ER_Recovery.Application.Services;
using ER_Recovery.Infrastructure.Data;
using ER_Recovery.Infrastructure.Data.Repositories;
using ER_Recovery.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ER_Recovery.Infrastructure.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.DataProtection;

namespace ER_Recovery.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // HttpClient
            builder.Services.AddHttpClient<IDailyReflectionService, DailyReflectionService>();
            
            // DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Remove if we do not want email confirmation
            //builder.Services.AddDefaultIdentity<IdentityUser>(
            //    options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Services
            builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
            builder.Services.AddScoped<IMeetingsService, MeetingsService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            // Anti Forgery issue with Docker
            var keyPath = Path.Combine(Directory.GetCurrentDirectory(), "keys");
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keyPath))
                .SetApplicationName("ER_Recovery.Web");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            // Add Authentication
            app.UseAuthentication();
            app.UseAuthorization();

            // Static files
            app.UseStaticFiles();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            DbInitializer.InitDb(app);

            app.Run();
        }
    }
}
