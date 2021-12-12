using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class ChangedDepartmentIdInUserEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_Promoter_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Promoter_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Promoter_DepartmentId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Promoter_DepartmentId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Promoter_DepartmentId",
                table: "AspNetUsers",
                column: "Promoter_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_Promoter_DepartmentId",
                table: "AspNetUsers",
                column: "Promoter_DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
