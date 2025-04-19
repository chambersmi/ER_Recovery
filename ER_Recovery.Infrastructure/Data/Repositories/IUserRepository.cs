﻿using ER_Recovery.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Infrastructure.Data.Repositories
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllUsersAsync();
    }
}
