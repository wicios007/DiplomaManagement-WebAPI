using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebAPI.Entities
{
    public class DiplomaManagementDbContext : IdentityDbContext<User, Role, int>
    {

        public DiplomaManagementDbContext(DbContextOptions<DiplomaManagementDbContext> options) : base(options)
        {
            
        }

        public DbSet<Thesis> Theses { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
/*            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();*/

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .ToTable("AspNetUsers")
                .HasDiscriminator<int>("UserType")
                .HasValue<User>((int) RoleValue.User)
                .HasValue<Promoter>((int) RoleValue.Promoter)
                .HasValue<Student>((int) RoleValue.Student)
                .HasValue<Admin>((int) RoleValue.Admin);

            modelBuilder.Entity<Thesis>()
                .Property(r => r.Name)
                .IsRequired();
            modelBuilder.Entity<College>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Department>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(r => r.City)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Address>()
                .Property(r => r.Street)
                .IsRequired()
                .HasMaxLength(50);


        }

    }
}
