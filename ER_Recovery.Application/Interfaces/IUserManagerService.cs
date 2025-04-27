using ER_Recovery.Domains.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Interfaces
{
    public interface IUserManagerService
    {
        Task<List<UserDTO>> GetAllUsersWithRoles();
        Task<bool> DeleteUserByIdAsync(string id);
        Task<string> GetUserHandleAsync(string userId);
    }
}
