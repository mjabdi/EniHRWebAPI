using Microsoft.EntityFrameworkCore.Migrations;

namespace EniHRWebAPI.Migrations
{
    public partial class MyDBContexthousingchanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UtilitiesIncluded",
                table: "Housing",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "UtilitiesIncluded",
                table: "Housing",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
