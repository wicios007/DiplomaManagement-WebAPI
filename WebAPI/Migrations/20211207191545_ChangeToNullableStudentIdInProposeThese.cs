using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class ChangeToNullableStudentIdInProposeThese : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposedTheses_AspNetUsers_StudentId",
                table: "ProposedTheses");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "ProposedTheses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedTheses_AspNetUsers_StudentId",
                table: "ProposedTheses",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposedTheses_AspNetUsers_StudentId",
                table: "ProposedTheses");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "ProposedTheses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedTheses_AspNetUsers_StudentId",
                table: "ProposedTheses",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
