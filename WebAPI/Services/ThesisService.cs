using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class ThesisService : IThesisService
    {
        private readonly DiplomaManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public ThesisService(DiplomaManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ThesisDto> GetAll(int departmentId)
        {
            var departments = GetDepartmentById(departmentId);
            if (departments == null)
            {
                throw new NotFoundException("Department not found");
            }
            var theses = _dbContext.Theses.Where(x => x.DepartmentId == departmentId).ToList();

            var listOfTheses = _mapper.Map<List<ThesisDto>>(theses);

            return listOfTheses;
        }

        public ThesisDto GetById(int departmentId, int thesisId)
        {
            var departments = GetDepartmentById(departmentId);

            var thesis = _dbContext
                .Theses
                .FirstOrDefault(t => t.Id == thesisId);

            var dto = _mapper.Map<Thesis, ThesisDto>(thesis);

            return dto;

        }

        public ThesisDto GetByUserId(int departmentId, int userId)
        {
            var departments = GetDepartmentById(departmentId);
            if(departments is null)
            {
                throw new NotFoundException("Department not found");
            }
            var thesis = _dbContext
                .Theses
                .FirstOrDefault(t => t.StudentId == userId);

            var dto = _mapper.Map<Thesis, ThesisDto>(thesis);
            return dto;
        }

        public int Create(int departmentId, ThesisDto dto)
        {
            var department = GetDepartmentById(departmentId);
            if(department == null)
            {
                throw new NotFoundException("department not found");
            }
            var thesis = _mapper.Map<Thesis>(dto);

            _dbContext.Theses.Add(thesis);
            _dbContext.SaveChanges();

            return thesis.Id;

        }

        public void Update(int departmentId, int thesisId, ThesisDto dto)
        {
            var department = GetDepartmentById(departmentId);

            if (department == null || department.Id != departmentId)
            {
                throw new NotFoundException("Department not found");
            }

            var thesis = _dbContext
                .Theses
                .FirstOrDefault(t => t.Id == thesisId);

            thesis.Name = dto.Name;
            thesis.NameEnglish = dto.NameEnglish;
            thesis.Description = dto.Description;
            //thesis.IsTaken = dto.IsTaken;

            /*
             tutaj powinienem dodać jeszcze zmianę id studenta oraz promotora, tylko musialbym to dodać w dtosach ?
             */

            _dbContext.SaveChanges();
        }

        public void Delete(int departmentId, int thesisId)
        {
            var department = GetDepartmentById(departmentId);

            if (department == null || department.Id != departmentId)
            {
                throw new NotFoundException("College or department not found");
            }

            var thesis = _dbContext
                .Theses
                .FirstOrDefault(t => t.Id == thesisId);

            _dbContext.Remove(thesis);
            _dbContext.SaveChanges();

        }

        public void DeleteAll(int departmentId)
        {
            var department = GetDepartmentById(departmentId);

            if (department == null || department.Id != departmentId)
            {
                throw new NotFoundException("College or department not found");
            }
            var theses = _dbContext.Theses.Where(x => x.DepartmentId == departmentId).ToList();

            _dbContext.RemoveRange(theses);
            _dbContext.SaveChanges();
        }

        public void GenerateCard(ThesisCardDto dto)
        {
            
        }

        


        private Department GetDepartmentById(int departmentId)
        {
            var department = _dbContext.
                Departments.
                FirstOrDefault(d => d.Id == departmentId);

            if (department == null || department.Id != departmentId)
            {
                throw new NotFoundException("Department not found");
            }

            return department;

        }

    }
}
