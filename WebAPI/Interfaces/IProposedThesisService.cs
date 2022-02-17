using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IProposedThesisService
    {
        int Create(ProposedThesisDto dto);
        void Delete(int id);
        void Update(int id, ProposedThesisUpdateDto dto);
        ProposedThesisDto GetById(int departmentId, int id);
        int Accept(int departmentId, int proposedThesisId);
        List<ProposedThesisDto> GetAll();
        List<ProposedThesisDto> GetAllFromDepartment(int departmentId);
        List<ProposedThesisDto> GetByStudentId(int departmentId, int studentId);
        List<ProposedThesisDto> GetByPromoterId(int departmentId, int promoterId);
        List<ProposedThesisDto> GetAddedByPromoters(int departmentId);
    }
}
