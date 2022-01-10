using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI
{
    public static class DiplomaManagementUserSeeder
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        private static void SeedUsers(UserManager<User> userManager)
        {
            if(userManager.FindByEmailAsync("admin@admin.com").Result == null)
            {
                Admin admin = new Admin
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    RegistrationDate = DateTime.Now
                };
                string password = "Admin1234";
                IdentityResult result = userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }
            }
        }
        private static void SeedRoles(RoleManager<Role> roleManager)
        {

            List<Role> roles = GetRoles();
            foreach (var item in roles)
            {
                if (roleManager.RoleExistsAsync(item.Name).Result == false)
                {
                    IdentityResult result = roleManager.CreateAsync(item).Result;
                }
                
            }
        }

        private static List<Role> GetRoles()
        {
            var userRoleList = new List<Role>
                {
                    new Role
                    {
                        //Id = 1,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "User",
                        NormalizedName = "USER",
                        RoleValue = RoleValue.User
                    },
                    new Role
                    {
                        //Id = 2,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "Promoter",
                        NormalizedName = "PROMOTER",
                        RoleValue = RoleValue.Promoter
                    },
                    new Role
                    {
                        //Id = 3,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "Student",
                        NormalizedName = "STUDENT",
                        RoleValue = RoleValue.Student
                    },
                    new Role
                    {
                        //Id = 4,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        RoleValue = RoleValue.Admin
                    }
                };
            return userRoleList;
        }
    }
}
