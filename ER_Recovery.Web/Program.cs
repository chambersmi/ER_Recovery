using ER_Recovery.Application.Interfaces;
using ER_Recovery.Application.Services;
using ER_Recovery.Infrastructure;
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

            // Add Infrastructure
            builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

            // Services
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
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

                DbInitializer.InitDb(app);

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
