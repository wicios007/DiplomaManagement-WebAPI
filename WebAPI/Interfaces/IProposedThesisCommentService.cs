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

        ProposedTheseCommentDto GetById(int id);

        int Create(ProposedTheseCommentDto dto);

        void Update(int id, UpdateProposedTheseCommentDto dto);
        void Delete(int id);
    }
}
