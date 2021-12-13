using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IProposedThesisCommentService
    {
        List<ProposedTheseCommentDto> GetAll();
        List<ProposedTheseCommentDto> GetAllThesisComments(int departmentId, int proposedThesisId);
        ProposedTheseCommentDto GetById(int departmentId, int proposedThesisId, int commentId);
        int Create(int departmentId, int proposedThesisId, ProposedTheseCommentDto dto);
        void Update(int departmentId, int proposedThesisId, int commentId, UpdateProposedTheseCommentDto dto);
        void Delete(int id);

    }
}
