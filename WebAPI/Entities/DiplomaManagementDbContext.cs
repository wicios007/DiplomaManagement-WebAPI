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
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProposedThese> ProposedTheses{get;set;}
        public DbSet<ProposedTheseComment> ProposedTheseComments{get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<ProposedThese>()
                .Property(r => r.Name)
                .IsRequired();
                
            modelBuilder.Entity<ProposedTheseComment>();
                

            modelBuilder.Entity<Department>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

        }

    }
}
