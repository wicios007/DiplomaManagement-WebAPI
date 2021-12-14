using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Exceptions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class ProposedThesisCommentService : IProposedThesisCommentService
    {
        private readonly IMapper mapper;
        private readonly DiplomaManagementDbContext dbContext;
        private readonly ILogger<ProposedThesisCommentService> logger;
        private readonly IProposedThesisService proposedThesisService;
        private readonly UserManager<User> userManager;
        private readonly IUserContextService userContextService;

        public ProposedThesisCommentService(DiplomaManagementDbContext _dbContext, IMapper _mapper, ILogger<ProposedThesisCommentService> _logger, IProposedThesisService _proposedThesisService, UserManager<User> _userManager, IUserContextService _userContextService)
        {
            dbContext = _dbContext;
            mapper = _mapper;
            logger = _logger;
            proposedThesisService = _proposedThesisService;
            userManager = _userManager;
            userContextService = _userContextService;
        }
        public List<ProposedTheseCommentDto> GetAll()
        {
            var comments = dbContext.ProposedTheseComments.ToList();
            var result = mapper.Map<List<ProposedTheseCommentDto>>(comments);
            return result;
        }
        public List<ProposedTheseCommentDto> GetAllThesisComments(int departmentId, int proposedThesisId)
        {
            var comments = dbContext.ProposedTheseComments
                .Where(c => (c.ProposedTheseId == proposedThesisId && c.DepartmentId == departmentId))
                .ToList();
            var result = mapper.Map<List<ProposedTheseCommentDto>>(comments);
            return result;
        }
        public ProposedTheseCommentDto GetById(int departmentId, int proposedThesisId, int commentId)
        {
            var proposedThesis = proposedThesisService.GetById(departmentId, proposedThesisId);
            var comment = dbContext.ProposedTheseComments.FirstOrDefault(c => c.Id == commentId);
            var result = mapper.Map<ProposedTheseCommentDto>(comment);
            return result;
        }

        public int Create(int departmentId, int proposedThesisId, ProposedTheseCommentDto dto)
        {
            var department = dbContext.Departments.FirstOrDefault(c => c.Id == departmentId);
            var thesis = dbContext.ProposedTheses.FirstOrDefault(c => c.Id == proposedThesisId);

            var comment = mapper.Map<ProposedTheseComment>(dto);
            var user = userManager.Users.FirstOrDefault(c => c.Id == userContextService.GetUserId);
            comment.CreatedBy = user;
            comment.CreatedById = user.Id;

            comment.ProposedThese = thesis;
            comment.ProposedTheseId = thesis.Id;

            comment.Department = department;
            comment.DepartmentId = department.Id;

            dbContext.ProposedTheseComments.Add(comment);
            
            dbContext.SaveChanges();
            return comment.Id;
        }

        public void Update(int departmentId, int proposedThesisId, int commentId, UpdateProposedTheseCommentDto dto)
        {
            var propThesis = proposedThesisService.GetById(departmentId, proposedThesisId);
            var comment = dbContext.ProposedTheseComments.FirstOrDefault(c => (c.Id == commentId && c.ProposedTheseId == proposedThesisId));
            if(comment is null)
            {
                throw new NotFoundException("Comment not found");
            }
            comment.Comment = dto.Comment;

            dbContext.SaveChanges();

        }

        public void Delete(int id)
        {
            var comment = dbContext.ProposedTheseComments.FirstOrDefault(c => c.Id == id);
            if(comment is null)
            {
                throw new NotFoundException("comment not found");
            }
            dbContext.Remove(comment);
            dbContext.SaveChanges();
        }

    }
}
