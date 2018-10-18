using Microsoft.EntityFrameworkCore.Migrations;

namespace EniHRWebAPI.Migrations
{
    public partial class MyDBContexthousingchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualFurnitureCosts",
                table: "Housing",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DifferenceAllowanceMonthlyCostsPaid",
                table: "Housing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FurnishedUnFurnished",
                table: "Housing",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FurnitureAllowance",
                table: "Housing",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ParkingCharges",
                table: "Housing",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RegularPayrollDeduction",
                table: "Housing",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UtilitiesIncluded",
                table: "Housing",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualFurnitureCosts",
                table: "Housing");

            migrationBuilder.DropColumn(
                name: "DifferenceAllowanceMonthlyCostsPaid",
                table: "Housing");

            migrationBuilder.DropColumn(
                name: "FurnishedUnFurnished",
                table: "Housing");

            migrationBuilder.DropColumn(
                name: "FurnitureAllowance",
                table: "Housing");

            migrationBuilder.DropColumn(
                name: "ParkingCharges",
                table: "Housing");

            migrationBuilder.DropColumn(
                name: "RegularPayrollDeduction",
                table: "Housing");

            migrationBuilder.DropColumn(
                name: "UtilitiesIncluded",
                table: "Housing");
        }
    }
}
