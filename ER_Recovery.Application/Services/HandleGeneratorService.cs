using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using Microsoft.AspNetCore.Identity;

using ER_Recovery.Infrastructure.Data.Repositories;

namespace ER_Recovery.Application.Services
{
    public class HandleGeneratorService : IHandleGeneratorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public HandleGeneratorService(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<string> GenerateUniqueAnonymousHandleAsync()
        {
            const string baseHandle = "Anonymous";
            var random = new Random();

            for(int i=0; i < 1000; i++)
            {
                var randomDigits = random.Next(0, 1000).ToString("D3");
                var newHandle = $"{baseHandle}{randomDigits}";

                var exists = await _userRepository.HandleExistsAsync(newHandle);

                if(!exists)
                {
                    return newHandle;
                }
            }

            throw new Exception("Could not generate a unique anonymous handle.");
        }
    }
}
