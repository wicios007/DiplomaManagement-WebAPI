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
                .ForMember(dest => dest.LastName, y => y.MapFrom(src => src.LastName))
                .ForPath(dest => dest.IndexNumber, y => y.MapFrom(src => src.Student.IndexNumber))
                .ForPath(dest => dest.Title, y => y.MapFrom(src => src.Promoter.Title));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Email, y => y.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, y => y.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, y => y.MapFrom(src => src.LastName))
                .ForPath(dest => dest.Promoter.Title, y => y.MapFrom(src => src.Title))
                .ForPath(dest => dest.Student.IndexNumber, y => y.MapFrom(src => src.IndexNumber));

            CreateMap<ProposedThesisDto, ProposedThese>();
            CreateMap<ProposedThese, ProposedThesisDto>();

            CreateMap<ProposedTheseComment, ProposedTheseCommentDto>();
            CreateMap<ProposedTheseCommentDto, ProposedTheseComment>();

            CreateMap<ProposedThese, Thesis>();
            CreateMap<Thesis, ProposedThese>();
            CreateMap<ThesisDto, ProposedThese>();
            CreateMap<ProposedThese, ThesisDto>();
        }
        
    }
}
