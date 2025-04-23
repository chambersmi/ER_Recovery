using ER_Recovery.Domains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Services
{
    public class HandleGeneratorService : IHandleGeneratorService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HandleGeneratorService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GenerateUniqueAnonymousHandleAsync()
        {
            const string baseHandle = "Anonymous";
            var random = new Random();

            for(int i=0; i < 1000; i++)
            {
                var randomDigits = random.Next(0, 1000).ToString("D3");
                var newHandle = $"{baseHandle}{randomDigits}";

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserHandle == newHandle);

                if(user==null)
                {
                    return newHandle;
                }
            }

            throw new Exception("Could not generate a unique anonymous handle.");
        }
    }
}
