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

            // Set Env or Production environments
            //builder.Configuration
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false)
            //    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            //    .AddEnvironmentVariables();

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

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Services
            builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
            builder.Services.AddScoped<IMeetingsService, MeetingsService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserManagerService, UserManagerService>();
            builder.Services.AddScoped<IHandleGeneratorService, HandleGeneratorService>();

            // Anti Forgery issue with Docker            
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("/app/keys"))
                .SetApplicationName("EatonRapidsRecoveryApp");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                DbInitializer.InitDb(app);
            }


            // Static files
            app.UseStaticFiles();


            app.UseHttpsRedirection();

            app.UseRouting();
            // Add Authentication
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
