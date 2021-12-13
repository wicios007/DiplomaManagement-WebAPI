using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI
{
    public class DiplomaManagementMappingProfile : Profile
    {
        public DiplomaManagementMappingProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();
            

            CreateMap<Thesis, ThesisDto>();
            CreateMap<ThesisDto, Thesis>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Email, y => y.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, y => y.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, y => y.MapFrom(src => src.LastName));
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Email, y => y.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, y => y.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, y => y.MapFrom(src => src.LastName));

            CreateMap<ProposedThesisDto, ProposedThese>();
            CreateMap<ProposedThese, ProposedThesisDto>();
        }
        
    }
}
