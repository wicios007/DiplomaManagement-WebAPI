using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/college")]
    [ApiController]
    [Authorize]
    public class CollegeController : ControllerBase
    {
        private readonly ICollegeService _collegeService;

        public CollegeController(ICollegeService collegeService)
        {
            _collegeService = collegeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<CollegeDto>> GetAll()
        {
            var collegeDtos = _collegeService.GetAllColleges();
            return Ok(collegeDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CollegeDto> Get([FromRoute] int id)
        {
            var college = _collegeService.GetById(id);
            return Ok(college);
        }

        [HttpPost]
        public ActionResult CreateCollege([FromBody] CreateCollegeDto dto)
        {
            var id = _collegeService.Create(dto);
            return Created($"/api/college/{id}", null);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateCollege([FromBody] UpdateCollegeDto dto, [FromRoute] int id)
        {
            _collegeService.Update(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCollege([FromRoute] int id)
        {
            _collegeService.Delete(id);
            return NoContent();
        }

    }
}
