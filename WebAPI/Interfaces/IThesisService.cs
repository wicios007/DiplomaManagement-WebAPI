using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IThesisService
    {
        List<ThesisDto> GetAll(int departmentId);
        ThesisDto GetById(int departmentId, int thesisId);
        ThesisDto GetByUserId(int departmentId, int userId);
        int Create(int departmentId, ThesisDto dto);
        public void Update(int departmentId, int thesisId, ThesisDto dto);
        public void Delete(int departmentId, int thesisId);
        public void DeleteAll(int departmentId);
    }
}