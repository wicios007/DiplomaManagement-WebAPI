using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProposedThesisCommentService(DiplomaManagementDbContext _dbContext, IMapper _mapper, ILogger<ProposedThesisCommentService> _logger)
        {
            dbContext = _dbContext;
            mapper = _mapper;
            logger = _logger;
        }
        public List<ProposedTheseCommentDto> GetAll()
        {
            var comments = dbContext.ProposedTheseComments.ToList();
            var result = mapper.Map<List<ProposedTheseCommentDto>>(comments);
            return result;
        }
        public ProposedTheseCommentDto GetById(int id)
        {
            var comment = dbContext.ProposedTheseComments.FirstOrDefault(c => c.Id == id);
            var result = mapper.Map<ProposedTheseCommentDto>(comment);
            return result;
        }

        public int Create(ProposedTheseCommentDto dto)
        {
            var comment = mapper.Map<ProposedTheseComment>(dto);
            dbContext.ProposedTheseComments.Add(comment);
            dbContext.SaveChanges();
            return comment.Id;
        }

        public void Update(int id, UpdateProposedTheseCommentDto dto)
        {
            var comment = dbContext.ProposedTheseComments.FirstOrDefault(c => c.Id == id);
            if(comment is null)
            {
                throw new NotFoundException("Comment not found");
            }
            comment.Comment = dto.Comment;
            comment.StudentId = dto.StudentId;
            comment.PromoterId = dto.PromoterId;

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
