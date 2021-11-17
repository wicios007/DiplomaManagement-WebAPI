﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Entities;

namespace WebAPI.Migrations
{
    [DbContext(typeof(DiplomaManagementDbContext))]
    [Migration("20211112180209_VirtualDepartmentRemovedInThesis")]
    partial class VirtualDepartmentRemovedInThesis
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("WebAPI.Entities.College", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Colleges");
                });

            modelBuilder.Entity("WebAPI.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CollegeId")
                        .HasColumnType("int");

                    b.Property<string>("Initials")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CollegeId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("WebAPI.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebAPI.Entities.Thesis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTaken")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PromoterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PromoterId");

                    b.ToTable("Theses");
                });

            modelBuilder.Entity("WebAPI.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("WebAPI.Entities.Promoter", b =>
                {
                    b.HasBaseType("WebAPI.Entities.User");

                    b.Property<int?>("CollegeId")
                        .HasColumnType("int")
                        .HasColumnName("Promoter_CollegeId");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int")
                        .HasColumnName("Promoter_DepartmentId");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("CollegeId");

                    b.HasIndex("DepartmentId");

                    b.HasDiscriminator().HasValue("Promoter");
                });

            modelBuilder.Entity("WebAPI.Entities.Student", b =>
                {
                    b.HasBaseType("WebAPI.Entities.User");

                    b.Property<int?>("CollegeId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("IndexNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ThesisId")
                        .HasColumnType("int");

                    b.HasIndex("CollegeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ThesisId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("WebAPI.Entities.College", b =>
                {
                    b.HasOne("WebAPI.Entities.Address", "Address")
                        .WithOne("College")
                        .HasForeignKey("WebAPI.Entities.College", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("WebAPI.Entities.Department", b =>
                {
                    b.HasOne("WebAPI.Entities.College", null)
                        .WithMany("Departments")
                        .HasForeignKey("CollegeId");
                });

            modelBuilder.Entity("WebAPI.Entities.Thesis", b =>
                {
                    b.HasOne("WebAPI.Entities.Promoter", null)
                        .WithMany("Theses")
                        .HasForeignKey("PromoterId");
                });

            modelBuilder.Entity("WebAPI.Entities.User", b =>
                {
                    b.HasOne("WebAPI.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WebAPI.Entities.Promoter", b =>
                {
                    b.HasOne("WebAPI.Entities.College", "College")
                        .WithMany()
                        .HasForeignKey("CollegeId");

                    b.HasOne("WebAPI.Entities.Department", "Department")
                        .WithMany("Promoters")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("College");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("WebAPI.Entities.Student", b =>
                {
                    b.HasOne("WebAPI.Entities.College", "College")
                        .WithMany()
                        .HasForeignKey("CollegeId");

                    b.HasOne("WebAPI.Entities.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("WebAPI.Entities.Thesis", "Thesis")
                        .WithMany()
                        .HasForeignKey("ThesisId");

                    b.Navigation("College");

                    b.Navigation("Department");

                    b.Navigation("Thesis");
                });

            modelBuilder.Entity("WebAPI.Entities.Address", b =>
                {
                    b.Navigation("College");
                });

            modelBuilder.Entity("WebAPI.Entities.College", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("WebAPI.Entities.Department", b =>
                {
                    b.Navigation("Promoters");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("WebAPI.Entities.Promoter", b =>
                {
                    b.Navigation("Theses");
                });
#pragma warning restore 612, 618
        }
    }
}
