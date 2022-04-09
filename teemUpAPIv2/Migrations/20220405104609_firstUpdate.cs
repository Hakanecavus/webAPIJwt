using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teemUpAPIv2.Migrations
{
    public partial class firstUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
