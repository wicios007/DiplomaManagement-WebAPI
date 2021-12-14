using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddedCreatedByIdFieldInCommentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposedTheseComments_AspNetUsers_PromoterId",
                table: "ProposedTheseComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposedTheseComments_AspNetUsers_StudentId",
                table: "ProposedTheseComments");

            migrationBuilder.DropIndex(
                name: "IX_ProposedTheseComments_PromoterId",
                table: "ProposedTheseComments");

            migrationBuilder.DropIndex(
                name: "IX_ProposedTheseComments_StudentId",
                table: "ProposedTheseComments");

            migrationBuilder.DropColumn(
                name: "PromoterId",
                table: "ProposedTheseComments");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "ProposedTheseComments");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ProposedTheseComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProposedTheseComments_CreatedById",
                table: "ProposedTheseComments",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedTheseComments_AspNetUsers_CreatedById",
                table: "ProposedTheseComments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposedTheseComments_AspNetUsers_CreatedById",
                table: "ProposedTheseComments");

            migrationBuilder.DropIndex(
                name: "IX_ProposedTheseComments_CreatedById",
                table: "ProposedTheseComments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ProposedTheseComments");

            migrationBuilder.AddColumn<int>(
                name: "PromoterId",
                table: "ProposedTheseComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "ProposedTheseComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProposedTheseComments_PromoterId",
                table: "ProposedTheseComments",
                column: "PromoterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposedTheseComments_StudentId",
                table: "ProposedTheseComments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedTheseComments_AspNetUsers_PromoterId",
                table: "ProposedTheseComments",
                column: "PromoterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedTheseComments_AspNetUsers_StudentId",
                table: "ProposedTheseComments",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
