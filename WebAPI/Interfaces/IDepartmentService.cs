using System.Collections.Generic;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IDepartmentService
    {
        List<DepartmentDto> GetAll();
        int Create(CreateDepartmentDto dto);
        void Delete(int departmentId);
        void DeleteAll();
        DepartmentDto GetById(int departmentId);
        void Update(int departmentId, UpdateDepartmentDto dto);
    }
}