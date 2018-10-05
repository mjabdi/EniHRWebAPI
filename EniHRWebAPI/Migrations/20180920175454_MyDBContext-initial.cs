using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EniHRWebAPI.Migrations
{
    public partial class MyDBContextinitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityStatus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentStatus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusinessUnit",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FamilyStatus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HomeCompany",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeCompany", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LocalPlus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalPlus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationUnit",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationUnit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParentPosition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentPosition", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalArea",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalArea", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReportsTo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportsTo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StaffTypology",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffTypology", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StandardEmployeeCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardEmployeeCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfVISA",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfVISA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkingCostCentre",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingCostCentre", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true),
                    CityCountryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                    table.ForeignKey(
                        name: "FK_City_Country_CityCountryID",
                        column: x => x.CityCountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    LocalPlusID = table.Column<int>(nullable: true),
                    staffTypologyID = table.Column<int>(nullable: true),
                    standardEmployeeCategoryID = table.Column<int>(nullable: true),
                    locationID = table.Column<int>(nullable: true),
                    HRECode = table.Column<long>(nullable: true),
                    Secretary = table.Column<string>(nullable: true),
                    businessUnitID = table.Column<int>(nullable: true),
                    WorkingComapny = table.Column<string>(nullable: true),
                    organisationUnitID = table.Column<int>(nullable: true),
                    workingCostCentreID = table.Column<string>(nullable: true),
                    positionID = table.Column<string>(nullable: true),
                    parentPositionID = table.Column<int>(nullable: true),
                    cosID = table.Column<int>(nullable: true),
                    projectID = table.Column<int>(nullable: true),
                    familyStatusID = table.Column<int>(nullable: true),
                    activityStatusID = table.Column<int>(nullable: true),
                    professionalAreaID = table.Column<int>(nullable: true),
                    SuperVisor = table.Column<string>(nullable: true),
                    ReportsToID = table.Column<int>(nullable: true),
                    CompanyHiringDate = table.Column<DateTime>(nullable: true),
                    homeCompanyID = table.Column<int>(nullable: true),
                    GenderString = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    CountryofBirthID = table.Column<int>(nullable: true),
                    NationalityID = table.Column<int>(nullable: true),
                    cityID = table.Column<int>(nullable: true),
                    assignmentStatusID = table.Column<int>(nullable: true),
                    FollowingPartner = table.Column<bool>(nullable: true),
                    FollowingChildren = table.Column<int>(nullable: false),
                    SpouseNationalityID = table.Column<int>(nullable: true),
                    SpouseName = table.Column<string>(nullable: true),
                    SpouseDateofBirth = table.Column<DateTime>(nullable: true),
                    typeOfVISAID = table.Column<int>(nullable: true),
                    VISAExpirayDate = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Country_CountryofBirthID",
                        column: x => x.CountryofBirthID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_LocalPlus_LocalPlusID",
                        column: x => x.LocalPlusID,
                        principalTable: "LocalPlus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Country_NationalityID",
                        column: x => x.NationalityID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_ReportsTo_ReportsToID",
                        column: x => x.ReportsToID,
                        principalTable: "ReportsTo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Country_SpouseNationalityID",
                        column: x => x.SpouseNationalityID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_ActivityStatus_activityStatusID",
                        column: x => x.activityStatusID,
                        principalTable: "ActivityStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_AssignmentStatus_assignmentStatusID",
                        column: x => x.assignmentStatusID,
                        principalTable: "AssignmentStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_BusinessUnit_businessUnitID",
                        column: x => x.businessUnitID,
                        principalTable: "BusinessUnit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_City_cityID",
                        column: x => x.cityID,
                        principalTable: "City",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_COS_cosID",
                        column: x => x.cosID,
                        principalTable: "COS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_FamilyStatus_familyStatusID",
                        column: x => x.familyStatusID,
                        principalTable: "FamilyStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_HomeCompany_homeCompanyID",
                        column: x => x.homeCompanyID,
                        principalTable: "HomeCompany",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Location_locationID",
                        column: x => x.locationID,
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_OrganisationUnit_organisationUnitID",
                        column: x => x.organisationUnitID,
                        principalTable: "OrganisationUnit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_ParentPosition_parentPositionID",
                        column: x => x.parentPositionID,
                        principalTable: "ParentPosition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Position_positionID",
                        column: x => x.positionID,
                        principalTable: "Position",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_ProfessionalArea_professionalAreaID",
                        column: x => x.professionalAreaID,
                        principalTable: "ProfessionalArea",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_StaffTypology_staffTypologyID",
                        column: x => x.staffTypologyID,
                        principalTable: "StaffTypology",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_StandardEmployeeCategory_standardEmployeeCategoryID",
                        column: x => x.standardEmployeeCategoryID,
                        principalTable: "StandardEmployeeCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_TypeOfVISA_typeOfVISAID",
                        column: x => x.typeOfVISAID,
                        principalTable: "TypeOfVISA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_WorkingCostCentre_workingCostCentreID",
                        column: x => x.workingCostCentreID,
                        principalTable: "WorkingCostCentre",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    EmployeeID = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Assignments_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Child",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChildName = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: true),
                    EmployeeID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Child", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Child_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Housing",
                columns: table => new
                {
                    HousingID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeID = table.Column<long>(nullable: true),
                    HomeAddressUK = table.Column<string>(nullable: true),
                    EntitledNumberofBedrooms = table.Column<int>(nullable: true),
                    ActualNumberofBedrooms = table.Column<int>(nullable: true),
                    TypeOfProperty = table.Column<string>(nullable: true),
                    RentDueDate = table.Column<int>(nullable: true),
                    TenancyAgreementStartDate = table.Column<DateTime>(nullable: true),
                    TenancyAgreementEndDate = table.Column<DateTime>(nullable: true),
                    MonthNoticePeriod = table.Column<string>(nullable: true),
                    InitialHouseContractRent = table.Column<decimal>(nullable: true),
                    CurrentHouseRental = table.Column<decimal>(nullable: true),
                    EntitledUnFurnishedAllowanceWeek = table.Column<decimal>(nullable: true),
                    TotalAllowance = table.Column<decimal>(nullable: true),
                    Furniture = table.Column<decimal>(nullable: true),
                    ActualHousingCosts = table.Column<decimal>(nullable: true),
                    HousingComments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Housing", x => x.HousingID);
                    table.ForeignKey(
                        name: "FK_Housing_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Child_EmployeeID",
                table: "Child",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_City_CityCountryID",
                table: "City",
                column: "CityCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CountryofBirthID",
                table: "Employees",
                column: "CountryofBirthID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LocalPlusID",
                table: "Employees",
                column: "LocalPlusID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationalityID",
                table: "Employees",
                column: "NationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReportsToID",
                table: "Employees",
                column: "ReportsToID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SpouseNationalityID",
                table: "Employees",
                column: "SpouseNationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_activityStatusID",
                table: "Employees",
                column: "activityStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_assignmentStatusID",
                table: "Employees",
                column: "assignmentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_businessUnitID",
                table: "Employees",
                column: "businessUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_cityID",
                table: "Employees",
                column: "cityID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_cosID",
                table: "Employees",
                column: "cosID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_familyStatusID",
                table: "Employees",
                column: "familyStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_homeCompanyID",
                table: "Employees",
                column: "homeCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_locationID",
                table: "Employees",
                column: "locationID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_organisationUnitID",
                table: "Employees",
                column: "organisationUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_parentPositionID",
                table: "Employees",
                column: "parentPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_positionID",
                table: "Employees",
                column: "positionID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_professionalAreaID",
                table: "Employees",
                column: "professionalAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_projectID",
                table: "Employees",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_staffTypologyID",
                table: "Employees",
                column: "staffTypologyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_standardEmployeeCategoryID",
                table: "Employees",
                column: "standardEmployeeCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_typeOfVISAID",
                table: "Employees",
                column: "typeOfVISAID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_workingCostCentreID",
                table: "Employees",
                column: "workingCostCentreID");

            migrationBuilder.CreateIndex(
                name: "IX_Housing_EmployeeID",
                table: "Housing",
                column: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Child");

            migrationBuilder.DropTable(
                name: "Housing");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LocalPlus");

            migrationBuilder.DropTable(
                name: "ReportsTo");

            migrationBuilder.DropTable(
                name: "ActivityStatus");

            migrationBuilder.DropTable(
                name: "AssignmentStatus");

            migrationBuilder.DropTable(
                name: "BusinessUnit");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "COS");

            migrationBuilder.DropTable(
                name: "FamilyStatus");

            migrationBuilder.DropTable(
                name: "HomeCompany");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "OrganisationUnit");

            migrationBuilder.DropTable(
                name: "ParentPosition");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "ProfessionalArea");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "StaffTypology");

            migrationBuilder.DropTable(
                name: "StandardEmployeeCategory");

            migrationBuilder.DropTable(
                name: "TypeOfVISA");

            migrationBuilder.DropTable(
                name: "WorkingCostCentre");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
