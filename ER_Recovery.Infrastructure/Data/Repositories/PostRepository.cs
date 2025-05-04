using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Infrastructure.Data.Repositories
{
    public class PostRepository<T> : IPostRepository<T> where T : class, IPost
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public PostRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddPostAsync(T post)
        {
            await _dbSet.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await GetPostByIdAsync(id);

            if (post == null)
                return false;

            _dbSet.Remove(post);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<T>> GetAllPostsAsync()
        {
            return await _dbSet.Include(p => p.User).ToListAsync();
        }

        public async Task<T?> GetPostByIdAsync(int id)
        {
            return await _dbSet.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<T> UpdateAsync(T post)
        {
            _dbSet.Update(post);
            await _context.SaveChangesAsync();

            return post;
        }
    }
}
