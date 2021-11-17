using System.Collections.Generic;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ICollegeService
    {
        int Create(CreateCollegeDto dto);
        void Update(int id, UpdateCollegeDto dto);
        void Delete(int id);
        List<CollegeDto> GetAllColleges();
        CollegeDto GetById(int id);
    }
}