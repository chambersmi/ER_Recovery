using ER_Recovery.Application.Services.Interfaces;
using ER_Recovery.Domains.Models;
using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserManagerService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<UserManagerService> logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<UserDTO>> GetAllUsersWithRoles()
        {
            var users = _userManager.Users.ToList();
            var usersDTO = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userDTO = new UserDTO
                {
                    User = user,
                    Roles = roles.ToList()
                };

                usersDTO.Add(userDTO);
            }

            return usersDTO;
            
        }

        public async Task<bool> DeleteUserByIdAsync(string id)
        {
            if (id != null)
            {
                return await _userRepository.DeleteUserByIdAsync(id);
            }

            return false;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<string> GetUserHandleAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            return user.UserHandle;
        }
    }
}
