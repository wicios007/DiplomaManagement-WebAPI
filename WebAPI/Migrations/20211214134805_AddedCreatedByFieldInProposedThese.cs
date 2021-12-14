using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddedCreatedByFieldInProposedThese : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ProposedTheses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProposedTheses_CreatedById",
                table: "ProposedTheses",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedTheses_AspNetUsers_CreatedById",
                table: "ProposedTheses",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposedTheses_AspNetUsers_CreatedById",
                table: "ProposedTheses");

            migrationBuilder.DropIndex(
                name: "IX_ProposedTheses_CreatedById",
                table: "ProposedTheses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ProposedTheses");
        }
    }
}
