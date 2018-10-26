using Microsoft.EntityFrameworkCore.Migrations;

namespace EniHRWebAPI.Migrations
{
    public partial class MyDBContexthousingchanges3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Furniture",
                table: "Housing",
                newName: "Deposit");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Housing",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Housing");

            migrationBuilder.RenameColumn(
                name: "Deposit",
                table: "Housing",
                newName: "Furniture");
        }
    }
}
