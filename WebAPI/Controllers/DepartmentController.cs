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
    [Route("api/department")]
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
        public ActionResult<List<DepartmentDto>> GetAll()
        {
            var departments = _departmentService.GetAll();

            return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        public ActionResult<DepartmentDto> Get([FromRoute] int departmentId)
        {
            var department = _departmentService.GetById(departmentId);
            return Ok(department);
        }
        [HttpDelete("{departmentId}")]
        public ActionResult Delete([FromRoute] int departmentId)
        {
            _departmentService.Delete(departmentId);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteAll()
        {
            _departmentService.DeleteAll();
            return NoContent();
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreateDepartmentDto dto)
        {
            var newDepartmentId = _departmentService.Create(dto);

            return Created($"api/department/{newDepartmentId}", null);
        }

        [HttpPut("{departmentId}")]
        public ActionResult Put([FromRoute] int departmentId, [FromBody] UpdateDepartmentDto dto)
        {
            _departmentService.Update(departmentId, dto);
            return Ok();
        }


    }
}
