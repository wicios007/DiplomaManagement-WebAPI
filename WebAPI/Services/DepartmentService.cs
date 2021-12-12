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

        public List<DepartmentDto> GetAll()
        {
            var departments = _dbContext.Departments.ToList();
            var departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);

            return departmentDtos;

        }

        public int Create(CreateDepartmentDto dto)
        {
            var departmentEntity = _mapper.Map<Department>(dto);

            departmentEntity.Initials = GetInitials(dto.Name);

            _dbContext.Departments.Add(departmentEntity);
            _dbContext.SaveChanges();

            return departmentEntity.Id;
        }

        public void Delete(int departmentId)
        {
            var department = _dbContext
                .Departments
                .SingleOrDefault(c => c.Id == departmentId);

            if (department == null)
            {
                throw new NotFoundException("Department not found");
            }

            _dbContext.Remove(department);
            _dbContext.SaveChanges();
        }

        public void DeleteAll()
        {
            var departments = _dbContext.Departments.ToList();

            _dbContext.RemoveRange(departments);
            _dbContext.SaveChanges();
        }

        public void Update(int departmentId, UpdateDepartmentDto dto)
        {
            var department = _dbContext
                .Departments
                .SingleOrDefault(c => c.Id == departmentId);
            if(department == null)
            {
                throw new NotFoundException("Department not found");
            }

            department.Name = dto.Name;
            department.Initials = GetInitials(dto.Name);
            _mapper.Map<UpdateDepartmentDto, Department>(dto);

            _dbContext.SaveChanges();
        }


        public DepartmentDto GetById(int departmentId)
        {
            var department = _dbContext
                .Departments
                .FirstOrDefault(d => d.Id == departmentId);
            if (department == null)
                throw new NotFoundException("Department not found");

            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
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
