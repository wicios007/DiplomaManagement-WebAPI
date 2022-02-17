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
    [Route("api/department/{departmentId}/proposedThesis")]
    [ApiController]
    [Authorize]
    public class ProposedThesisController : ControllerBase
    {
        public readonly IProposedThesisService proposedThesisService;
        public ProposedThesisController(IProposedThesisService _proposedThesisService)
        {
            proposedThesisService = _proposedThesisService;
        }
        [HttpGet]
        public ActionResult<List<ProposedThesisDto>> GetAll([FromRoute] int departmentId)
        {
            var result = proposedThesisService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProposedThesisDto> GetById([FromRoute] int departmentId, [FromRoute]int id)
        {
            var result = proposedThesisService.GetById(departmentId, id);
            return Ok(result);
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Update([FromRoute] int departmentId, [FromRoute] int id, ProposedThesisUpdateDto dto)
        {
            proposedThesisService.Update(id, dto);
            return Ok();
        }
        [HttpPost]
        public ActionResult<ProposedThesisDto> Create([FromRoute]int departmentId, ProposedThesisDto dto)
        {
            var id = proposedThesisService.Create(dto);
            return Created($"api/department/{departmentId}/proposedThesis/{id}", null);
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteById([FromRoute]int id)
        {
            proposedThesisService.Delete(id);
            return NoContent();
        }
        [HttpGet]
        [Route("student/{studentId}")]
        public ActionResult<List<ProposedThesisDto>> GetByStudentId([FromRoute] int departmentId, [FromRoute] int studentId)
        {
            var result = proposedThesisService.GetByStudentId(departmentId, studentId);
            return Ok(result);
        }
        [HttpGet]
        [Route("promoter/{promoterId}")]
        public ActionResult<List<ProposedThesisDto>> GetByPromoterId([FromRoute] int departmentId, [FromRoute] int promoterId)
        {
            var result = proposedThesisService.GetByPromoterId(departmentId, promoterId);
            return Ok(result);
        }

        [HttpPost]
        [Route("{proposedThesisId}/accept")]
        [Authorize(Roles = "Promoter, Student")]
        public ActionResult AcceptThesis([FromRoute] int departmentId, [FromRoute] int proposedThesisId)
        {
            var acceptedId = proposedThesisService.Accept(departmentId, proposedThesisId);
            return Created($"api/department/{departmentId}/thesis/{acceptedId}", null);
        }
        [HttpGet]
        [Route("promoter")]
        [Authorize(Roles = "Promoter, Student")]
        public ActionResult<List<ProposedThesisDto>> GetAllByPromoter([FromRoute] int departmentId)
        {
            var theses = proposedThesisService.GetAddedByPromoters(departmentId);
            return Ok(theses);
        }
        
    }
}
