using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

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
                /*var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())*/
                {
                    if (!_dbContext.Roles.Any())
                    {
                        IEnumerable<Role> roles = GetRoles();
                        _dbContext.Roles.AddRange(roles);
                        _dbContext.SaveChanges();
                    }

                    if (!_dbContext.Theses.Any())
                    {
                        IEnumerable<Thesis> theses = GetTheses();
                        _dbContext.Theses.AddRange(theses);
                        _dbContext.SaveChanges();
                    }

                    if (!_dbContext.Colleges.Any())
                    {
                        IEnumerable<College> colleges = GetColleges();
                        _dbContext.Colleges.AddRange(colleges);
                        _dbContext.SaveChanges();
                    }

                }
            }
        }

        private static IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "Promoter"
                },
                new Role()
                {
                    Name = "Student"
                }
            };

            return roles;
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
                    IsTaken = true
                },
                new Thesis()
                {
                    Name = "Projekt sieci",
                    NameEnglish = "Sample",
                    Description =
                        "SAMPLE Stworzenie projektu sieci",
                    IsTaken = false
                },

            };
            return theses;
        }

        private static IEnumerable<College> GetColleges()
        {
            var colleges = new List<College>()
            {
                new College()
                {
                    Name = "Politechnika Częstochowska",
                    Address = new Address()
                    {
                        City = "Częstochowa",
                        Street = "Generała Jana Henryka Dąbrowskiego 69",
                        PostalCode = "42-201"
                    },
                    Departments = new List<Department>()
                    {
                        new Department()
                        {
                            Name = "Wydział Inżynierii Mechanicznej i Informatyki",
                            Initials = "WIMII"
                        },
                        new Department()
                        {
                            Name = "Wydział Zarządzania",
                            Initials = "WZ"
                        },
                        new Department()
                        {
                            Name = "Wydział Budownictwa",
                            Initials = "WB"
                        }
                    }
                }
            };
            return colleges;
        }

        
    }
}