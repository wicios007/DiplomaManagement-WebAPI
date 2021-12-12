using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddedCollegeAndDepartmentInStudentAndPromoterEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Promoter_CollegeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CollegeId",
                table: "AspNetUsers",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Promoter_CollegeId",
                table: "AspNetUsers",
                column: "Promoter_CollegeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Colleges_CollegeId",
                table: "AspNetUsers",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Colleges_Promoter_CollegeId",
                table: "AspNetUsers",
                column: "Promoter_CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Colleges_CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Colleges_Promoter_CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Promoter_CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Promoter_CollegeId",
                table: "AspNetUsers");
        }
    }
}
