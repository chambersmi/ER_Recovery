using ER_Recovery.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Interfaces
{
    public interface IPostRepository<T> where T : class, IPost
    {
        Task<List<T>> GetAllPostsAsync();
        Task<T?> GetPostByIdAsync(int id);
        Task<T> AddPostAsync(T post);
        Task<T> UpdateAsync(T post);
        Task<bool> DeletePostAsync(int id);
    }
}
