using AutoMapper;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Entities;

namespace ProjectApi.Application.AutoMapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, Customer>()
                .ForAllMembers ( opt => 
                {
                    opt.Condition((src, dest, sourceMember) => sourceMember != null);
                });
        }
    }
}