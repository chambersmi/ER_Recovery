using ER_Recovery.Application.Interfaces;
using ER_Recovery.Application.Services;
using ER_Recovery.Domains.Entities;
using ER_Recovery.Infrastructure.Data.Repositories;
using ER_Recovery.Infrastructure.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {


            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // Add Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Repositories
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageBoardRepository, MessageBoardRepository>();

            // Services
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IUserManagerService, UserManagerService>();



            return services;
        }
    }
}
