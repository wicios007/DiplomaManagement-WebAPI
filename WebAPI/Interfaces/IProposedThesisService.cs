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
        void Update(int id, ProposedThesisDto dto);
        ProposedThesisDto GetById(int id);
        List<ProposedThesisDto> GetAll();
        List<ProposedThesisDto> GetAllFromDepartment(int departmentId);

    }
}
