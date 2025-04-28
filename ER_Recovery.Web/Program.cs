using ER_Recovery.Application.Interfaces;
using ER_Recovery.Application.Services;
using ER_Recovery.Domains.Entities;
using ER_Recovery.Infrastructure;
using ER_Recovery.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddScoped<IHandleGeneratorService, HandleGeneratorService>();
            builder.Services.AddScoped<ISobrietyDateService, SobrietyDateService>();
            builder.Services.AddScoped<IMeetingsService, MeetingsService>();
            builder.Services.AddScoped<IMessageBoardService, MessageBoardService>();


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
