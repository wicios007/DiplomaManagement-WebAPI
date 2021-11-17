using System.Collections.Generic;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IDepartmentService
    {
        List<DepartmentDto> GetAll(int collegeId);
        int Create(int collegeId, CreateDepartmentDto dto);
        void Delete(int collegeId, int departmentId);
        void DeleteAll(int collegeId);
        DepartmentDto GetById(int collegeId, int departmentId);
        void Update(int collegeId, int departmentId, UpdateDepartmentDto dto);
    }
}