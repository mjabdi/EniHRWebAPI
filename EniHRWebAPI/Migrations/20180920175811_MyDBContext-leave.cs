using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EniHRWebAPI.Migrations
{
    public partial class MyDBContextleave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    LeaveID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeID = table.Column<long>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    UntilDate = table.Column<DateTime>(nullable: false),
                    CountedDays = table.Column<int>(nullable: false),
                    LeaveType = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    AttachmentUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.LeaveID);
                    table.ForeignKey(
                        name: "FK_Leaves_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_EmployeeID",
                table: "Leaves",
                column: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaves");
        }
    }
}
