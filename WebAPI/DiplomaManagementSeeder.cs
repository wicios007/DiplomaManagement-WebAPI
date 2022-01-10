using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI
{
    public class DiplomaManagementSeeder
    {
        private readonly DiplomaManagementDbContext _dbContext;

        public DiplomaManagementSeeder(DiplomaManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                    if (!_dbContext.Departments.Any())
                    {
                        IEnumerable<Department> departments = GetDepartments();
                        _dbContext.Departments.AddRange(departments);
                        _dbContext.SaveChanges();
                    }
            }
        }

        private static IEnumerable<Department> GetDepartments()
        {
            var departments = new List<Department>()
            {
                new Department()
                {
                    Name = "Wydział Inżynierii Mechanicznej i Informatyki",
                    Initials = "WIMII"
                },
                new Department()
                {
                    Name = "Wydział Inżynierii Produkcji",
                    Initials = "WIP"
                },
                new Department()
                {
                    Name = "Wydział Budownictwa",
                    Initials = "WB"
                },
            };
            return departments;
        }

        private static IEnumerable<ProposedThese> GetProposedTheses()
        {
            var propTheses = new List<ProposedThese>()
            {
                new ProposedThese()
                {
                    Name = "Aplikacja do zarządzania pracami dyplomowymi",
                    NameEnglish = "Application for theses management",
                    Description = "Stworzenie aplikacji do zarządzania pracami dyplomowymi z użyciem frameworków .NET 5.0 oraz Angular",
                    IsAccepted = false
                },
                new ProposedThese()
                {
                    Name = "Projekt sieci",
                    NameEnglish = "Sample",
                    Description =
                        "SAMPLE Stworzenie projektu sieci",
                    IsAccepted = false
                }
            };
            return propTheses;
        }

        private static IEnumerable<Thesis> GetTheses()
        {
            var theses = new List<Thesis>()
            {
                new Thesis()
                {
                    Name = "Aplikacja do zarządzania pracami dyplomowymi",
                    NameEnglish = "Application for theses management",
                    Description =
                        "Stworzenie aplikacji do zarządzania pracami dyplomowymi z użyciem frameworków .NET 5.0 oraz Angular",
                },
                new Thesis()
                {
                    Name = "Projekt sieci",
                    NameEnglish = "Sample",
                    Description =
                        "SAMPLE Stworzenie projektu sieci",
                },

            };
            return theses;
        }


        
    }
}