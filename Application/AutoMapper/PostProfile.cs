using AutoMapper;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostDto, Post>()
                .ForAllMembers(opt =>
                {
                    opt.Condition((src, dest, sourceMember) => sourceMember != null);
                });
        }
    }
}