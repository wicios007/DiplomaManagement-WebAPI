using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddedListOfThesesInDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Theses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theses_DepartmentId",
                table: "Theses",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theses_Departments_DepartmentId",
                table: "Theses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theses_Departments_DepartmentId",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Theses_DepartmentId",
                table: "Theses");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Theses");
        }
    }
}
