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
            CreateMap<College, CollegeDto>()
                .ForMember(m => m.City, c => c.MapFrom(c => c.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(c => c.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(c => c.Address.PostalCode));

            CreateMap<CreateCollegeDto, College>()
                .ForMember(c => c.Address, m => m.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    Street = dto.Street,
                    PostalCode = dto.PostalCode
                }));
            CreateMap<UpdateCollegeDto, College>()
                .ForMember(c => c.Address, m => m.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    Street = dto.Street,
                    PostalCode = dto.PostalCode
                }));
            CreateMap<Department, DepartmentDto>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, DepartmentDto>();

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
        }
        
    }
}
