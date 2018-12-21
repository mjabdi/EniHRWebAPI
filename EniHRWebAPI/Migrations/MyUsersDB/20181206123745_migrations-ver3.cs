using Microsoft.EntityFrameworkCore.Migrations;

namespace EniHRWebAPI.Migrations.MyUsersDB
{
    public partial class migrationsver3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RolesStr",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechnicalID",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RolesStr",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TechnicalID",
                table: "Users");
        }
    }
}
