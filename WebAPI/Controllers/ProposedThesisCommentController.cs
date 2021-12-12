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
    [Route("api/department/{departmentId}/proposedThesis/comment")]
    [ApiController]
    public class ProposedThesisCommentController : ControllerBase
    {
        private readonly IProposedThesisCommentService commentService;

        public ProposedThesisCommentController(IProposedThesisCommentService _commentService)
        {
            commentService = _commentService;
        }
        [HttpGet]
        public ActionResult<List<ProposedTheseCommentDto>> GetAll([FromRoute] int departmentId)
        {
            var result = commentService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("all")]
        public ActionResult<List<ProposedTheseCommentDto>> GetAll()
        {
            var result = commentService.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{commentId}")]
        public ActionResult<ProposedTheseCommentDto> GetById([FromRoute] int departmentId, [FromRoute] int commentId)
        {
            var result = commentService.GetById(commentId);
            return result;
        }
        [HttpPost]
        public ActionResult Create([FromRoute] int departmentId, [FromBody] ProposedTheseCommentDto dto)
        {
            var id = commentService.Create(dto);
            return Created($"api/department/{departmentId}/proposedThesis/comment/{id}", null);
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Update()
        {
            return Ok(); //TODO::
        }

    }
}
