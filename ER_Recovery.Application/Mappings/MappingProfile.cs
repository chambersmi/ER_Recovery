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
        }
    }
}
