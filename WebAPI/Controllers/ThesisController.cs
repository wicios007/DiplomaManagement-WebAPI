using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/college/{collegeId}/department/{departmentId}/thesis")]
    [ApiController]
    //[Authorize]
    public class ThesisController : ControllerBase
    {
        private readonly IThesisService _thesisService;

        public ThesisController(IThesisService thesisService)
        {
            _thesisService = thesisService;
        }

        [HttpGet]
        public ActionResult<List<ThesisDto>> GetAll([FromRoute]int collegeId, [FromRoute]int departmentId)
        {
            var result = _thesisService.GetAll(collegeId, departmentId);

            return Ok(result);
        }

        [HttpGet]
        [Route("{thesisId}")]
        public ActionResult<ThesisDto> Get([FromRoute] int collegeId, [FromRoute] int departmentId,
            [FromRoute] int thesisId)
        {
            var thesis = _thesisService.GetById(collegeId, departmentId, thesisId);

            return Ok(thesis);
        }

        [HttpPut]
        [Route("{thesisId}")]
        public ActionResult Update([FromRoute] int collegeId, [FromRoute] int departmentId, [FromRoute] int thesisId, [FromBody] ThesisDto dto)
        {
            _thesisService.Update(collegeId, departmentId, thesisId, dto);
            return Ok();
        }

        [HttpPost]
        public ActionResult<ThesisDto> Create([FromRoute] int collegeId, [FromRoute] int departmentId,
            [FromBody] ThesisDto dto)
        {
            var id = _thesisService.Create(collegeId, departmentId, dto);
            return Created($"api/college/{collegeId}/department/{departmentId}/thesis/{id}", null);
        }

        [HttpDelete]
        public ActionResult DeleteAll([FromRoute] int collegeId, [FromRoute] int departmentId)
        {
            _thesisService.DeleteAll(collegeId, departmentId);
            return NoContent();
        }

        [HttpDelete]
        [Route("{thesisId}")]
        public ActionResult DeleteById([FromRoute] int collegeId, [FromRoute] int departmentId,
            [FromRoute] int thesisId)
        {
            _thesisService.Delete(collegeId,departmentId,thesisId);
            return NoContent();
        }


    }
}
