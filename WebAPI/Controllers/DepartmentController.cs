using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/college/{collegeId}/department")]
    [ApiController]
   // [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public ActionResult<List<DepartmentDto>> GetAll([FromRoute] int collegeId)
        {
            var departments = _departmentService.GetAll(collegeId);

            return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        public ActionResult<DepartmentDto> Get([FromRoute] int collegeId, [FromRoute] int departmentId)
        {
            var department = _departmentService.GetById(collegeId, departmentId);
            return Ok(department);
        }
        [HttpDelete("{departmentId}")]
        public ActionResult Delete([FromRoute] int collegeId, [FromRoute] int departmentId)
        {
            _departmentService.Delete(collegeId, departmentId);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteAll([FromRoute] int collegeId)
        {
            _departmentService.DeleteAll(collegeId);
            return NoContent();
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int collegeId, [FromBody] CreateDepartmentDto dto)
        {
            var newDepartmentId = _departmentService.Create(collegeId, dto);

            return Created($"api/college/{collegeId}/department/{newDepartmentId}", null);
        }

        [HttpPut("{departmentId}")]
        public ActionResult Put([FromRoute] int collegeId, [FromRoute] int departmentId, [FromBody] UpdateDepartmentDto dto)
        {
            _departmentService.Update(collegeId, departmentId, dto);
            return Ok();
        }


    }
}
