using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using WebAPI.Entities;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Interfaces;


namespace WebAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DiplomaManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public DepartmentService(DiplomaManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<DepartmentDto> GetAll(int collegeId)
        {
            var college = GetCollegeById(collegeId);
            var departmentDtos = _mapper.Map<List<DepartmentDto>>(college.Departments);

            return departmentDtos;

        }

        public int Create(int collegeId, CreateDepartmentDto dto)
        {
            var college = GetCollegeById(collegeId);
            var departmentEntity = _mapper.Map<Department>(dto);

            departmentEntity.CollegeId = collegeId; //college.Id 
            departmentEntity.Initials = GetInitials(dto.Name);

            _dbContext.Departments.Add(departmentEntity);
            _dbContext.SaveChanges();

            return departmentEntity.Id;
        }

        public void Delete(int collegeId, int departmentId)
        {
            var college = GetCollegeById(collegeId);

            var department = _dbContext
                .Departments
                .SingleOrDefault(c => c.Id == departmentId);

            if (department == null || department.CollegeId != collegeId)
            {
                throw new NotFoundException("Department not found");
            }

            _dbContext.Remove(department);
            _dbContext.SaveChanges();
        }

        public void DeleteAll(int collegeId)
        {
            var college = GetCollegeById(collegeId);

            _dbContext.RemoveRange(college.Departments);
            _dbContext.SaveChanges();
        }

        public void Update(int collegeId, int departmentId, UpdateDepartmentDto dto)
        {
            var college = GetCollegeById(collegeId);
            var department = _dbContext
                .Departments
                .SingleOrDefault(c => c.Id == departmentId);
            if(department == null || department.CollegeId != collegeId)
            {
                throw new NotFoundException("Department not found");
            }

            department.Name = dto.Name;
            department.Initials = GetInitials(dto.Name);
            _mapper.Map<UpdateDepartmentDto, Department>(dto);

            _dbContext.SaveChanges();
        }


        public DepartmentDto GetById(int collegeId, int departmentId)
        {
            var college = GetCollegeById(collegeId);
            var department = _dbContext
                .Departments
                .FirstOrDefault(d => d.Id == departmentId);
            if (department == null || department.CollegeId != collegeId)
                throw new NotFoundException("Department not found");

            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        private College GetCollegeById(int collegeId)
        {
            var college = _dbContext
                .Colleges
                .Include(c => c.Departments)
                .FirstOrDefault(c => c.Id == collegeId);

            if(college == null)
            {
                throw new NotFoundException("College not found");
            }
            return college;
        }

        private static string GetInitials(string name)
        {
            string[] nameSplit = name.Split(new string[] {",", " "}, StringSplitOptions.RemoveEmptyEntries);
            string nameInitials = "";
            foreach (string item in nameSplit)
            {
                nameInitials += item.Substring(0, 1).ToUpper();
            }

            return nameInitials;
        }
    }
}
