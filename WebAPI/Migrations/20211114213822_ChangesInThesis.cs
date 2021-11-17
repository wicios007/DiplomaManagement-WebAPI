using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class ChangesInThesis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theses_Users_PromoterId",
                table: "Theses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Theses_ThesisId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ThesisId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "PromoterId",
                table: "Theses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Theses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Theses_StudentId",
                table: "Theses",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Theses_Users_PromoterId",
                table: "Theses",
                column: "PromoterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theses_Users_StudentId",
                table: "Theses",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theses_Users_PromoterId",
                table: "Theses");

            migrationBuilder.DropForeignKey(
                name: "FK_Theses_Users_StudentId",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Theses_StudentId",
                table: "Theses");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Theses");

            migrationBuilder.AddColumn<int>(
                name: "ThesisId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PromoterId",
                table: "Theses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ThesisId",
                table: "Users",
                column: "ThesisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theses_Users_PromoterId",
                table: "Theses",
                column: "PromoterId",
                principalTable: "Users",
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
    }
}
