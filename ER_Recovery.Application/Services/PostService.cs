using AutoMapper;
using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace ER_Recovery.Application.Services
{
    public class PostService<TEntity, TReadDto, TEditDto, TCreateDto> : IPostService<TReadDto, TEditDto, TCreateDto>
        where TEntity : class, IPost
        where TReadDto : class
        where TCreateDto : class
        where TEditDto : class
    {
        private readonly IPostRepository<TEntity> _postRepository;
        private IMapper _mapper;
        private readonly ILogger<PostService<TEntity, TReadDto, TEditDto, TCreateDto>> _logger;

        public PostService(
            IPostRepository<TEntity> postRepository, 
            ILogger<PostService<TEntity, TReadDto, TEditDto, TCreateDto>> logger,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TReadDto> AddPostAsync(TCreateDto dto)
        {            
            var entity = _mapper.Map<TEntity>(dto);
            var addPost = await _postRepository.AddPostAsync(entity);

            return _mapper.Map<TReadDto>(addPost); 
        }

        public async Task<TReadDto> AddPostWithUserAsync(TCreateDto dto, string userId, string userHandle)
        {            
            var entity = _mapper.Map<TEntity>(dto);
            entity.UserId = userId;
            entity.UserHandle = userHandle;

            var addPost = await _postRepository.AddPostAsync(entity);

            return _mapper.Map<TReadDto>(addPost);
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var existingPost = await _postRepository.GetPostByIdAsync(id);
            if(existingPost == null)
            {
                _logger.LogError($"Error: Could not delete {nameof(existingPost)}");
                return false;
            }

            return await _postRepository.DeletePostAsync(id);
        }

        public async Task<List<TReadDto>> GetAllPostsAsync()
        {
            var entities = await _postRepository.GetAllPostsAsync();

            return _mapper.Map<List<TReadDto>> (entities);

        }

        public async Task<TReadDto?> GetPostByIdAsync(int id)
        {
            var entity = await _postRepository.GetPostByIdAsync(id);

            if(entity == null)
            {
                _logger.LogError($"Error: Could not find by id {nameof(entity)}");
                throw new ArgumentNullException(nameof(entity));
            }

            return _mapper.Map<TReadDto>(entity);
        }

        public async Task<TReadDto> UpdatePostAsync(TEditDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            if(entity == null)
            {
                _logger.LogError($"Error: Could not update {nameof(entity)}");
                throw new ArgumentNullException(nameof(entity));
            }

            var updatedEntity = await _postRepository.UpdateAsync(entity);

            return _mapper.Map<TReadDto>(updatedEntity);
        }
    }
}
