using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //[Route("api/department/{departmentId}/proposedThesis/comment")]
    [Route("api/department/{departmentId}/proposedThesis/{proposedThesisId}/comment")]
    [ApiController]
    [Authorize]
    public class ProposedThesisCommentController : ControllerBase
    {
        private readonly IProposedThesisCommentService commentService;

        public ProposedThesisCommentController(IProposedThesisCommentService _commentService)
        {
            commentService = _commentService;
        }
        [HttpGet]
        public ActionResult<List<ProposedTheseCommentDto>> GetAll([FromRoute] int departmentId, [FromRoute] int proposedThesisId)
        {
            var result = commentService.GetAllThesisComments(departmentId, proposedThesisId);
            return Ok(result);
        }
        [HttpGet]
        [Route("{commentId}")]
        public ActionResult<ProposedTheseCommentDto> GetById([FromRoute] int departmentId, [FromRoute] int proposedThesisId, [FromRoute] int commentId)
        {
            var result = commentService.GetById(departmentId, proposedThesisId, commentId);
            return result;
        }
        [HttpPost]
        public ActionResult Create([FromRoute] int departmentId, [FromRoute] int proposedThesisId, [FromBody] ProposedTheseCommentDto dto)
        {
            var id = commentService.Create(departmentId, proposedThesisId, dto);
            return Created($"api/department/{departmentId}/proposedThesis/{proposedThesisId}/comment/{id}", null);
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Update([FromRoute] int departmentId, [FromRoute] int proposedThesisId, [FromRoute] int id, [FromBody] UpdateProposedTheseCommentDto dto)
        {
            commentService.Update(departmentId, proposedThesisId, id, dto);
            return Ok();
        }

    }
}
