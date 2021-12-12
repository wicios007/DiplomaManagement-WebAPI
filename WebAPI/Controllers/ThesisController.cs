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
    [Route("api/department/{departmentId}/thesis")]
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
        public ActionResult<List<ThesisDto>> GetAll([FromRoute]int departmentId)
        {
            var result = _thesisService.GetAll(departmentId);

            return Ok(result);
        }

        [HttpGet]
        [Route("{thesisId}")]
        public ActionResult<ThesisDto> Get([FromRoute] int departmentId,
            [FromRoute] int thesisId)
        {
            var thesis = _thesisService.GetById(departmentId, thesisId);

            return Ok(thesis);
        }

        [HttpPut]
        [Route("{thesisId}")]
        public ActionResult Update([FromRoute] int departmentId, [FromRoute] int thesisId, [FromBody] ThesisDto dto)
        {
            _thesisService.Update(departmentId, thesisId, dto);
            return Ok();
        }

        [HttpPost]
        public ActionResult<ThesisDto> Create([FromRoute] int departmentId,
            [FromBody] ThesisDto dto)
        {
            var id = _thesisService.Create(departmentId, dto);
            return Created($"api/department/{departmentId}/thesis/{id}", null);
        }

        [HttpDelete]
        public ActionResult DeleteAll([FromRoute] int departmentId)
        {
            _thesisService.DeleteAll(departmentId);
            return NoContent();
        }

        [HttpDelete]
        [Route("{thesisId}")]
        public ActionResult DeleteById([FromRoute] int departmentId,
            [FromRoute] int thesisId)
        {
            _thesisService.Delete(departmentId,thesisId);
            return NoContent();
        }


    }
}
