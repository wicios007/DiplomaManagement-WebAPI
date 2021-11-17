using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IThesisService
    {
        List<ThesisDto> GetAll(int collegeId, int departmentId);
        ThesisDto GetById(int collegeId, int departmentId, int thesisId);
        int Create(int collegeId, int departmentId, ThesisDto dto);
        public void Update(int collegeId, int departmentId, int thesisId, ThesisDto dto);
        public void Delete(int collegeId, int departmentId, int thesisId);
        public void DeleteAll(int collegeId, int departmentId);
    }
}