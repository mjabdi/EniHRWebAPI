using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EniHRWebAPI.Migrations
{
    public partial class MyDBContextleave2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredDataTime",
                table: "Leaves",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredDataTime",
                table: "Leaves");
        }
    }
}
