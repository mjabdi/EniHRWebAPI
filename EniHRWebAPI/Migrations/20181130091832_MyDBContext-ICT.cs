using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EniHRWebAPI.Migrations
{
    public partial class MyDBContextICT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ICTs",
                columns: table => new
                {
                    ICTID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeID = table.Column<long>(nullable: true),
                    ActiveDirAccount = table.Column<string>(nullable: true),
                    WorkstaionNumber = table.Column<string>(nullable: true),
                    ApprovalLevel1 = table.Column<string>(nullable: true),
                    ApprovalLevel2 = table.Column<string>(nullable: true),
                    MacroAggregation = table.Column<string>(nullable: true),
                    DeskPhoneNumber = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    AdvancedLyncProfile = table.Column<bool>(nullable: false),
                    SAP = table.Column<bool>(nullable: false),
                    LinkToAttachment = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    StartMoveDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICTs", x => x.ICTID);
                    table.ForeignKey(
                        name: "FK_ICTs_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ICTs_EmployeeID",
                table: "ICTs",
                column: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ICTs");
        }
    }
}
