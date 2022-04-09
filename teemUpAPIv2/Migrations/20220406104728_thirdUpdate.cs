using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teemUpAPIv2.Migrations
{
    public partial class thirdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "userDetails",
                columns: table => new
                {
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailVerified = table.Column<bool>(type: "bit", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDetails", x => x.email);
                    table.ForeignKey(
                        name: "FK_userDetails_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userDetails_userId",
                table: "userDetails",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userDetails");

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
