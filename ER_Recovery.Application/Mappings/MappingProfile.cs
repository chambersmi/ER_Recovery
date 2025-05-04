using AutoMapper;
using ER_Recovery.Application.DTOs;
using ER_Recovery.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MessageBoard, PostDTO>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AddMessageBoardDTO, MessageBoard>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserHandle, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedTime, opt => opt.Ignore());

            //CreateMap<AddMessageBoardDTO, MessageBoard>()
            //    .ForMember(dest => dest.UserId, opt => opt.Ignore())
            //    .ForMember(dest => dest.UserHandle, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedTime, opt => opt.Ignore());

            ////Output
            //CreateMap<MessageBoard, MessageBoardDTO>();

            //CreateMap<MessageBoard, PostDTO>()
            //    .ForMember(dest => dest.PostId, option => option.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.UserHandle, option => option.MapFrom(src => src.User.UserHandle))
            //    .ForMember(dest => dest.UserId, option => option.Ignore())
            //    .ForMember(dest => dest.UserHandle, option => option.Ignore())
            //    .ForMember(dest => dest.Title, option => option.MapFrom(src => src.Title))
            //    .ForMember(dest => dest.Content, option => option.MapFrom(src => src.Content))
            //    .ForMember(dest => dest.CreatedTime, option => option.MapFrom(src => src.CreatedTime));            

            //CreateMap<EditPostDTO, IPost>();
        }
    }
}
