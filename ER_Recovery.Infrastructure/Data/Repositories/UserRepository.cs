﻿using ER_Recovery.Domains.Entities;
using ER_Recovery.Domains.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(AppDbContext context, 
            ILogger<UserRepository> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<bool> DeleteUserByIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return true;
            }

            return false;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if(user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} was not found.");
            }

            return user;
        }

        public async Task<bool> HandleExistsAsync(string handle)
        {
            return await _context.Users.AnyAsync(u => u.UserName == handle);
        }
    }
}
