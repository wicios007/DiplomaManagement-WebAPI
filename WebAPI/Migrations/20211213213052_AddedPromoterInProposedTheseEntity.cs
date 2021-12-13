using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddedPromoterInProposedTheseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromoterId",
                table: "ProposedTheses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProposedTheses_PromoterId",
                table: "ProposedTheses",
                column: "PromoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedTheses_AspNetUsers_PromoterId",
                table: "ProposedTheses",
                column: "PromoterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposedTheses_AspNetUsers_PromoterId",
                table: "ProposedTheses");

            migrationBuilder.DropIndex(
                name: "IX_ProposedTheses_PromoterId",
                table: "ProposedTheses");

            migrationBuilder.DropColumn(
                name: "PromoterId",
                table: "ProposedTheses");
        }
    }
}
