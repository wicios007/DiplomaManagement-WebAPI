using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class NextMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Colleges_CollegeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Colleges_Promoter_CollegeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CollegeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Promoter_CollegeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Promoter_CollegeId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Promoter_CollegeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CollegeId",
                table: "Users",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Promoter_CollegeId",
                table: "Users",
                column: "Promoter_CollegeId");

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
        }
    }
}
