using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddedDepartmentIdInProposedThesisCommentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "ProposedTheseComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProposedTheseComments_DepartmentId",
                table: "ProposedTheseComments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedTheseComments_Departments_DepartmentId",
                table: "ProposedTheseComments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposedTheseComments_Departments_DepartmentId",
                table: "ProposedTheseComments");

            migrationBuilder.DropIndex(
                name: "IX_ProposedTheseComments_DepartmentId",
                table: "ProposedTheseComments");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "ProposedTheseComments");
        }
    }
}
