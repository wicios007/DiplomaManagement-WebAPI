using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Entities
{
    public class DiplomaManagementDbContext : DbContext
    {

        public DiplomaManagementDbContext(DbContextOptions<DiplomaManagementDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
/*        public DbSet<Promoter> Promoters { get; set; }
        public DbSet<Student> Students { get; set; }*/
        public DbSet<Role> Roles{ get; set; }
        public DbSet<Thesis> Theses { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();
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

/*            modelBuilder.Entity<Promoter>();
            modelBuilder.Entity<Student>();*/


        }

    }
}
