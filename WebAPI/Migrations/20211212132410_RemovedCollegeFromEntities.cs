using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class RemovedCollegeFromEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Colleges_CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Colleges_Promoter_CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Colleges_CollegeId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Colleges");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CollegeId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Promoter_CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Promoter_CollegeId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colleges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colleges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colleges_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CollegeId",
                table: "Departments",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CollegeId",
                table: "AspNetUsers",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Promoter_CollegeId",
                table: "AspNetUsers",
                column: "Promoter_CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Colleges_AddressId",
                table: "Colleges",
                column: "AddressId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Colleges_CollegeId",
                table: "Departments",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
