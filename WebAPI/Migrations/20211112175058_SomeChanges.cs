using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class SomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IndexNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Promoter_CollegeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Promoter_DepartmentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ThesisId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Theses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTaken = table.Column<bool>(type: "bit", nullable: false),
                    PromoterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Theses_Users_PromoterId",
                        column: x => x.PromoterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Colleges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colleges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colleges_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CollegeId",
                table: "Users",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Promoter_CollegeId",
                table: "Users",
                column: "Promoter_CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Promoter_DepartmentId",
                table: "Users",
                column: "Promoter_DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ThesisId",
                table: "Users",
                column: "ThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_Colleges_AddressId",
                table: "Colleges",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CollegeId",
                table: "Departments",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Theses_PromoterId",
                table: "Theses",
                column: "PromoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Colleges_CollegeId",
                table: "Users",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Colleges_Promoter_CollegeId",
                table: "Users",
                column: "Promoter_CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_Promoter_DepartmentId",
                table: "Users",
                column: "Promoter_DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Theses_ThesisId",
                table: "Users",
                column: "ThesisId",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Colleges_CollegeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Colleges_Promoter_CollegeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_Promoter_DepartmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Theses_ThesisId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Theses");

            migrationBuilder.DropTable(
                name: "Colleges");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Users_CollegeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Promoter_CollegeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Promoter_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ThesisId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IndexNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Promoter_CollegeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Promoter_DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Users");
        }
    }
}
