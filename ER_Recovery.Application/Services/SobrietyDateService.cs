using ER_Recovery.Application.Services.Interfaces;
using ER_Recovery.Domains.Models;
using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Services
{
    public class SobrietyDateService : ISobrietyDateService
    {
        public readonly IUserManagerService _userManagerService;
        public readonly ILogger<SobrietyDateService> _logger;

        public SobrietyDateService(IUserManagerService userManagerService, ILogger<SobrietyDateService> logger)
        {
            _userManagerService = userManagerService;
            _logger = logger;
        }

        public async Task<List<UserDTO>> GetSobrietyDateAsync()
        {
            var todaysDate = DateTime.Now.Date;

            // this may be overkill
            var users = await _userManagerService.GetAllUsersWithRoles();

            List<UserDTO> UserList = new List<UserDTO>();

            foreach(var user in users)
            {
                if(user.User.SobrietyDate == todaysDate)
                {
                    UserList.Add(user);
                }
            }

            return UserList;

            // Loop through userRepository and check if the Sobriety Date field matches todays date

        }
    }
}
