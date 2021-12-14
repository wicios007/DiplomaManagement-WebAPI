﻿using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<ProposedThesisDto> Create([FromRoute]int departmentId, ProposedThesisDto dto) //TODO:czy to tak powinno byc?
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
        public ActionResult<List<ProposedThesisDto>> GetByStudentId([FromRoute] int departmentId, [FromQuery] int studentId)
        {
            var result = proposedThesisService.GetByStudentId(departmentId, studentId);
            return Ok(result);
        }
        
    }
}