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
        private static UserManager<User> _userManager;
        private static RoleManager<Role> _roleManager;

        public DiplomaManagementSeeder(DiplomaManagementDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
 /*               var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())*/
                {

                    if (!_dbContext.Theses.Any())
                    {
                        IEnumerable<Thesis> theses = GetTheses();
                        _dbContext.Theses.AddRange(theses);
                        _dbContext.SaveChanges();
                    }
                    if (!_dbContext.ProposedTheses.Any())
                    {
                        IEnumerable<ProposedThese> propTheses = GetProposedTheses();
                        _dbContext.ProposedTheses.AddRange(propTheses);
                        _dbContext.SaveChanges();
                    }
                    if (!_roleManager.Roles.Any())
                    {
                        IEnumerable<Role> roles = GetRoles();
                        foreach(var item in roles)
                        {
                            _roleManager.CreateAsync(item);
                        }
                        _dbContext.SaveChanges();

                    }
                    if (!_userManager.Users.Any())
                    {

                    }

                }
                //SeedRoles();
                SeedAdmin();

            }
        }

        public static void SeedAdmin()
        {
            if(_userManager.FindByEmailAsync("admin@admin.com").Result == null)
            {
                var admin = new Admin
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    RegistrationDate = DateTime.Now

                };
                var password = "Admin1234";
                var result = _userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                {
                    var result2 = _userManager.AddToRoleAsync(admin, "Admin").Result;
                }
            }
        }

        public static IEnumerable<Role> GetRoles()
        {
            var userRoleList = new List<Role>
                {
                    new Role
                    {
                        Id = 0,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "User",
                        NormalizedName = "USER",
                        RoleValue = RoleValue.User
                    },
                    new Role
                    {
                        Id = 1,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "Promoter",
                        NormalizedName = "PROMOTER",
                        RoleValue = RoleValue.Promoter
                    },
                    new Role
                    {
                        Id = 2,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "Student",
                        NormalizedName = "STUDENT",
                        RoleValue = RoleValue.Student
                    },
                    new Role
                    {
                        Id = 3,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        RoleValue = RoleValue.Admin
                    }
                };
            return userRoleList;
        }

        public static void SeedRoles()
        {
            if (!_roleManager.Roles.Any())
            {
                var userRoleList = new List<Role>
                {
                    new Role
                    {
                        Id = 0,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "User",
                        NormalizedName = "USER",
                        RoleValue = RoleValue.User
                    },
                    new Role
                    {
                        Id = 1,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "Promoter",
                        NormalizedName = "PROMOTER",
                        RoleValue = RoleValue.Promoter
                    },
                    new Role
                    {
                        Id = 2,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "Student",
                        NormalizedName = "STUDENT",
                        RoleValue = RoleValue.Student
                    },
                    new Role
                    {
                        Id = 3,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        RoleValue = RoleValue.Admin
                    }
                };
                foreach (var item in userRoleList)
                {
                    _roleManager.CreateAsync(item);
                }
            }
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