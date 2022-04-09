using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teemUpAPIv2.Migrations
{
    public partial class secondUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "UserName");
        }
    }
}
