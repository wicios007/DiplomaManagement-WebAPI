using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Entities;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class CollegeService : ICollegeService
    {
        private readonly DiplomaManagementDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CollegeService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public CollegeService(DiplomaManagementDbContext dbContext, IMapper mapper, ILogger<CollegeService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public int Create(CreateCollegeDto dto)
        {
            College college = _mapper.Map<College>(dto);
            _dbContext.Colleges.Add(college);
            _dbContext.SaveChanges();
            return college.Id;

        }

        public void Update(int id, UpdateCollegeDto dto)
        {
            var college = _dbContext
                .Colleges
                .Include(c => c.Address)
                .FirstOrDefault(c => c.Id == id);


            if (college == null)
            {
                throw new NotFoundException("College not found");
            }

            college.Name = dto.Name;
            college.Address.City = dto.City;
            college.Address.Street = dto.Street;
            college.Address.PostalCode = dto.PostalCode;
            var mapped = _mapper.Map<UpdateCollegeDto, College>(dto);
            

            _dbContext.SaveChanges();

        }

        public void Delete(int id)
        {
            College college = _dbContext
                .Colleges
                .FirstOrDefault(c => c.Id == id);
            if (college == null)
                throw new NotFoundException("College not found");

            _dbContext.Colleges.Remove(college);
            _dbContext.SaveChanges();
        }

        public List<CollegeDto> GetAllColleges()
        {
            var colleges = _dbContext
                 .Colleges
                 .Include(c => c.Address)
                 .Include(c => c.Departments)
                 .ToList();

            var result = _mapper.Map<List<CollegeDto>>(colleges);
            return result;
        }
        public CollegeDto GetById(int id)
        {
            var college = _dbContext
                .Colleges
                .Include(c => c.Address)
                .Include(c => c.Departments)
                .FirstOrDefault(c => c.Id == id);

            if (college == null)
            {
                throw new NotFoundException($"College with id {id} has not been found");
            }

            var result = _mapper.Map<CollegeDto>(college);

            return result;
        }
    }
}
