using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProposedThesisService(DiplomaManagementDbContext _dbContext, IMapper _mapper, ILogger<ProposedThesisService> _logger, IDepartmentService _departmentService)
        {
            dbContext = _dbContext;
            mapper = _mapper;
            logger = _logger;
            departmentService = _departmentService;
        }

        public int Create(ProposedThesisDto dto)
        {
            ProposedThese propThese = mapper.Map<ProposedThese>(dto);
            dbContext.ProposedTheses.Add(propThese);
            dbContext.SaveChanges();
            return propThese.Id;
        }

        public void Delete(int id)
        {
            ProposedThese propThese = dbContext
                .ProposedTheses
                .FirstOrDefault(c => c.Id == id);
            if(propThese == null)
            {
                throw new NotFoundException("Proposed these not found");
            }
            dbContext.ProposedTheses.Remove(propThese);
            dbContext.SaveChanges();
        }
        public void Update(int id, ProposedThesisDto dto)
        {
            ProposedThese propThese = dbContext
                .ProposedTheses
                .FirstOrDefault(c => c.Id == id);
            if(propThese == null)
            {
                throw new NotFoundException("Proposed these not found");
            }
            propThese.Name = dto.Name;
            propThese.NameEnglish = dto.NameEnglish;
            propThese.Description = dto.Description;

            dbContext.SaveChanges();
        }

        public ProposedThesisDto GetById(int id)
        {
            var propThese = dbContext.ProposedTheses.FirstOrDefault(t => t.Id == id);
            if(propThese == null)
            {
                throw new NotFoundException("Proposed these not found");
            }
            var result = mapper.Map<ProposedThesisDto>(propThese);
            return result;
        }

        public List<ProposedThesisDto> GetAll()
        {
            
            var propTheses = dbContext.ProposedTheses.ToList();
            if(propTheses == null)
            {
                throw new NotFoundException("Proposed theses not found");
            }
            var result = mapper.Map<List<ProposedThesisDto>>(propTheses);
            return result;
        }

        public List<ProposedThesisDto> GetAllFromDepartment(int departmentId)
        {
            var department = departmentService.GetById(departmentId);
            if(department == null)
            {
                throw new NotFoundException("Department not found");
            }
            var propTheses = dbContext.ProposedTheses.Where(c => c.DepartmentId == departmentId).ToList();
            if(propTheses == null)
            {
                throw new NotFoundException("Proposed theses not found");
            }
            var result = mapper.Map<List<ProposedThesisDto>>(propTheses);
            return result;
        }
    }
}
