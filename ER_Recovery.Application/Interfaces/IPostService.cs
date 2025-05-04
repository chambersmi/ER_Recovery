using ER_Recovery.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Interfaces
{
    public interface IPostService<TReadDto, TEditDto, TCreateDto> 
        where TReadDto : class
        where TEditDto : class
        where TCreateDto : class
    {
        Task<List<TReadDto>> GetAllPostsAsync();
        Task<TReadDto?> GetPostByIdAsync(int id);
        Task<TReadDto> AddPostAsync(TCreateDto dto);
        Task<TReadDto> AddPostWithUserAsync(TCreateDto dto, string userId, string userHandle);
        Task<TReadDto> UpdatePostAsync(TEditDto dto);
        Task<bool> DeletePostAsync(int id);
    }
}
