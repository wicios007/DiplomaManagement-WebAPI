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

        public List<ThesisDto> GetAll(int collegeId, int departmentId)
        {
            var departments = GetDepartmentById(collegeId, departmentId);

            var listOfTheses = _mapper.Map<List<ThesisDto>>(departments.Theses);

            return listOfTheses;
        }

        public ThesisDto GetById(int collegeId, int departmentId, int thesisId)
        {
            var departments = GetDepartmentById(collegeId, departmentId);

            var thesis = _dbContext
                .Theses
                .FirstOrDefault(t => t.Id == thesisId);

            var dto = _mapper.Map<Thesis, ThesisDto>(thesis);

            return dto;

        }

        public int Create(int collegeId, int departmentId, ThesisDto dto)
        {
            var department = GetDepartmentById(collegeId, departmentId);

            var departmentEntity = _mapper.Map<Thesis>(dto);

            _dbContext.Theses.Add(departmentEntity);
            _dbContext.SaveChanges();

            return departmentEntity.Id;

        }

        public void Update(int collegeId, int departmentId, int thesisId, ThesisDto dto)
        {
            var department = GetDepartmentById(collegeId, departmentId);

            if (department == null || department.CollegeId != collegeId || department.Id != departmentId)
            {
                throw new NotFoundException("College or department not found");
            }

            var thesis = _dbContext
                .Theses
                .FirstOrDefault(t => t.Id == thesisId);

            thesis.Name = dto.Name;
            thesis.NameEnglish = dto.NameEnglish;
            thesis.Description = dto.Description;
            thesis.IsTaken = dto.IsTaken;

            /*
             tutaj powinienem dodać jeszcze zmianę id studenta oraz promotora, tylko musialbym to dodać w dtosach ?
             */

            _dbContext.SaveChanges();
        }

        public void Delete(int collegeId, int departmentId, int thesisId)
        {
            var department = GetDepartmentById(collegeId, departmentId);

            if (department == null || department.CollegeId != collegeId || department.Id != departmentId)
            {
                throw new NotFoundException("College or department not found");
            }

            var thesis = _dbContext
                .Theses
                .FirstOrDefault(t => t.Id == thesisId);

            _dbContext.Remove(thesis);
            _dbContext.SaveChanges();

        }

        public void DeleteAll(int collegeId, int departmentId)
        {
            var department = GetDepartmentById(collegeId, departmentId);

            if (department == null || department.CollegeId != collegeId || department.Id != departmentId)
            {
                throw new NotFoundException("College or department not found");
            }

            _dbContext.RemoveRange(department.Theses);
            _dbContext.SaveChanges();
        }



        private College GetCollegeById(int collegeId)
        {
            var college = _dbContext
                .Colleges
                .Include(c => c.Departments)
                .FirstOrDefault(c => c.Id == collegeId);
            return college;
        }

        private Department GetDepartmentById(int collegeId, int departmentId)
        {

            var department = _dbContext.
                Departments.
                FirstOrDefault(d => (d.Id == departmentId && d.CollegeId == collegeId));

            if (department == null || department.Id != departmentId || department.CollegeId != collegeId)
            {
                throw new NotFoundException("Department or college not found");
            }

            return department;

        }

    }
}
