using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddedProposedThesisListInDepartmentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "ProposedTheses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProposedTheses_DepartmentId",
                table: "ProposedTheses",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedTheses_Departments_DepartmentId",
                table: "ProposedTheses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposedTheses_Departments_DepartmentId",
                table: "ProposedTheses");

            migrationBuilder.DropIndex(
                name: "IX_ProposedTheses_DepartmentId",
                table: "ProposedTheses");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "ProposedTheses");
        }
    }
}
