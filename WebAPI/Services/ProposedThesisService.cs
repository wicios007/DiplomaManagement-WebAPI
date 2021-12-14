using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Exceptions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class ProposedThesisService : IProposedThesisService
    {
        private DiplomaManagementDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ProposedThesisService> logger;
        private readonly IDepartmentService departmentService;
        private readonly UserManager<User> userManager;
        private readonly IUserContextService userContextService;

        public ProposedThesisService(DiplomaManagementDbContext _dbContext, IMapper _mapper, ILogger<ProposedThesisService> _logger, IDepartmentService _departmentService, UserManager<User> _userManager, IUserContextService _userContextService)
        {
            dbContext = _dbContext;
            mapper = _mapper;
            logger = _logger;
            departmentService = _departmentService;
            userManager = _userManager;
            userContextService = _userContextService;
        }

        public int Create(ProposedThesisDto dto)
        {
            ProposedThese propThese = mapper.Map<ProposedThese>(dto);
            propThese.CreatedById = userContextService.GetUserId;
            var user = userManager.Users.FirstOrDefault(c => c.Id == userContextService.GetUserId);
            propThese.Student = (Student)user;
            propThese.StudentId = user.Id;
            
            /*switch (Role)
            {
                case "Student":
                    propThese.StudentId = Convert.ToInt32(UserId); //TODO: wykrzacza się, błąd z foreign key
                    break;
                case "Promoter":
                    propThese.PromoterId = Convert.ToInt32(UserId); //TODO: skoro góra się wykrzacza, to tutaj pewnie też
                    break;
                default:
                    break;
            }*/
            dbContext.ProposedTheses.Add(propThese);
            dbContext.SaveChanges();
            return propThese.Id;
        }

        public void Delete(int id)
        {
            ProposedThese propThese = dbContext
                .ProposedTheses
                .FirstOrDefault(c => c.Id == id);
            if (propThese == null)
            {
                throw new NotFoundException("Proposed these not found");
            }
            dbContext.ProposedTheses.Remove(propThese);
            dbContext.SaveChanges();
        }
        public void Update(int id, ProposedThesisUpdateDto dto)
        {
            ProposedThese propThese = dbContext
                .ProposedTheses
                .FirstOrDefault(c => c.Id == id);
            if (propThese == null)
            {
                throw new NotFoundException("Proposed these not found");
            }
            propThese.Name = dto.Name;
            propThese.NameEnglish = dto.NameEnglish;
            propThese.Description = dto.Description;
            propThese.CreatedById = userContextService.GetUserId;
            propThese.IsAccepted = dto.IsAccepted;
            dbContext.SaveChanges();
        }
/*        public void Accept(int id, ProposedThesisAcceptDto dto)
        {
            ProposedThese propThese = dbContext
                .ProposedTheses
                .FirstOrDefault(c => c.Id == id);
            if(propThese is null)
            {
                throw new NotFoundException("Proposed these not found");
            }
            propThese.IsAccepted = dto.accept;
            dbContext.SaveChanges();
        }
*/
        public ProposedThesisDto GetById(int departmentId, int id)
        {
            var propThese = dbContext.ProposedTheses.FirstOrDefault(t => t.Id == id);
            if (propThese == null)
            {
                throw new NotFoundException("Proposed these not found");
            }
            var result = mapper.Map<ProposedThesisDto>(propThese);
            return result;
        }

        public List<ProposedThesisDto> GetByStudentId(int departmentId, int studentId)
        {
            var propTheses = dbContext.ProposedTheses.Where(c => c.StudentId == studentId).ToList();
            var result = mapper.Map<List<ProposedThesisDto>>(propTheses);
            return result;
        }

        public List<ProposedThesisDto> GetAll()
        {
            var propTheses = dbContext.ProposedTheses.ToList();
            if (propTheses == null)
            {
                throw new NotFoundException("Proposed theses not found");
            }
            var result = mapper.Map<List<ProposedThesisDto>>(propTheses);

            return result;
        }

        public List<ProposedThesisDto> GetAllFromDepartment(int departmentId)
        {
            var department = departmentService.GetById(departmentId);
            if (department == null)
            {
                throw new NotFoundException("Department not found");
            }
            var propTheses = dbContext.ProposedTheses.Where(c => c.DepartmentId == departmentId).ToList();
            if (propTheses == null)
            {
                throw new NotFoundException("Proposed theses not found");
            }
            var result = mapper.Map<List<ProposedThesisDto>>(propTheses);
            return result;
        }
    }
}
