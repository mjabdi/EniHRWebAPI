﻿// <auto-generated />
using System;
using EniHRWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EniHRWebAPI.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20181018123454_MyDBContext-housingchanges2")]
    partial class MyDBContexthousingchanges2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EniHRWebAPI.Models.ActivityStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("ActivityStatus");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Assignment", b =>
                {
                    b.Property<long>("EmployeeID");

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("EmployeeID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.AssignmentStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("AssignmentStatus");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.BusinessUnit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("BusinessUnit");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Child", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChildName");

                    b.Property<DateTime?>("DateofBirth");

                    b.Property<long?>("EmployeeID");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Child");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CityCountryID");

                    b.Property<string>("CityName");

                    b.HasKey("ID");

                    b.HasIndex("CityCountryID");

                    b.ToTable("City");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.COS", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("COS");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryName");

                    b.HasKey("ID");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Employee", b =>
                {
                    b.Property<long>("EmployeeID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate");

                    b.Property<DateTime?>("CompanyHiringDate");

                    b.Property<int?>("CountryofBirthID");

                    b.Property<string>("EmailAddress");

                    b.Property<int>("FollowingChildren");

                    b.Property<bool?>("FollowingPartner");

                    b.Property<string>("GenderString");

                    b.Property<long?>("HRECode");

                    b.Property<int?>("LocalPlusID");

                    b.Property<string>("Name");

                    b.Property<int?>("NationalityID");

                    b.Property<int?>("ReportsToID");

                    b.Property<string>("Secretary");

                    b.Property<DateTime?>("SpouseDateofBirth");

                    b.Property<string>("SpouseName");

                    b.Property<int?>("SpouseNationalityID");

                    b.Property<string>("SuperVisor");

                    b.Property<string>("Surname");

                    b.Property<string>("UserId");

                    b.Property<DateTime?>("VISAExpirayDate");

                    b.Property<string>("WorkingComapny");

                    b.Property<int?>("activityStatusID");

                    b.Property<int?>("assignmentStatusID");

                    b.Property<int?>("businessUnitID");

                    b.Property<int?>("cityID");

                    b.Property<int?>("cosID");

                    b.Property<int?>("familyStatusID");

                    b.Property<int?>("homeCompanyID");

                    b.Property<int?>("locationID");

                    b.Property<int?>("organisationUnitID");

                    b.Property<int?>("parentPositionID");

                    b.Property<string>("positionID");

                    b.Property<int?>("professionalAreaID");

                    b.Property<int?>("projectID");

                    b.Property<int?>("staffTypologyID");

                    b.Property<int?>("standardEmployeeCategoryID");

                    b.Property<int?>("typeOfVISAID");

                    b.Property<string>("workingCostCentreID");

                    b.HasKey("EmployeeID");

                    b.HasIndex("CountryofBirthID");

                    b.HasIndex("LocalPlusID");

                    b.HasIndex("NationalityID");

                    b.HasIndex("ReportsToID");

                    b.HasIndex("SpouseNationalityID");

                    b.HasIndex("activityStatusID");

                    b.HasIndex("assignmentStatusID");

                    b.HasIndex("businessUnitID");

                    b.HasIndex("cityID");

                    b.HasIndex("cosID");

                    b.HasIndex("familyStatusID");

                    b.HasIndex("homeCompanyID");

                    b.HasIndex("locationID");

                    b.HasIndex("organisationUnitID");

                    b.HasIndex("parentPositionID");

                    b.HasIndex("positionID");

                    b.HasIndex("professionalAreaID");

                    b.HasIndex("projectID");

                    b.HasIndex("staffTypologyID");

                    b.HasIndex("standardEmployeeCategoryID");

                    b.HasIndex("typeOfVISAID");

                    b.HasIndex("workingCostCentreID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.FamilyStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("FamilyStatus");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.HomeCompany", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("HomeCompany");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Housing", b =>
                {
                    b.Property<long>("HousingID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("ActualFurnitureCosts");

                    b.Property<decimal?>("ActualHousingCosts");

                    b.Property<int?>("ActualNumberofBedrooms");

                    b.Property<decimal?>("CurrentHouseRental");

                    b.Property<decimal?>("DifferenceAllowanceMonthlyCostsPaid");

                    b.Property<long?>("EmployeeID");

                    b.Property<int?>("EntitledNumberofBedrooms");

                    b.Property<decimal?>("EntitledUnFurnishedAllowanceWeek");

                    b.Property<string>("FurnishedUnFurnished");

                    b.Property<decimal?>("Furniture");

                    b.Property<decimal?>("FurnitureAllowance");

                    b.Property<string>("HomeAddressUK");

                    b.Property<string>("HousingComments");

                    b.Property<decimal?>("InitialHouseContractRent");

                    b.Property<string>("MonthNoticePeriod");

                    b.Property<decimal?>("ParkingCharges");

                    b.Property<decimal?>("RegularPayrollDeduction");

                    b.Property<int?>("RentDueDate");

                    b.Property<DateTime?>("TenancyAgreementEndDate");

                    b.Property<DateTime?>("TenancyAgreementStartDate");

                    b.Property<decimal?>("TotalAllowance");

                    b.Property<string>("TypeOfProperty");

                    b.Property<string>("UtilitiesIncluded");

                    b.HasKey("HousingID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Housing");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Leave", b =>
                {
                    b.Property<long>("LeaveID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AttachmentUrl");

                    b.Property<string>("Comment");

                    b.Property<int>("CountedDays");

                    b.Property<long>("EmployeeID");

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("LeaveType");

                    b.Property<DateTime>("RegisteredDataTime");

                    b.Property<DateTime>("UntilDate");

                    b.HasKey("LeaveID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.LocalPlus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("LocalPlus");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.OrganisationUnit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("OrganisationUnit");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.ParentPosition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("ParentPosition");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Position", b =>
                {
                    b.Property<string>("ID");

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.ProfessionalArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("ProfessionalArea");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.ReportsTo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("ReportsTo");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.StaffTypology", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("StaffTypology");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.StandardEmployeeCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("StandardEmployeeCategory");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.TypeOfVISA", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("TypeOfVISA");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.WorkingCostCentre", b =>
                {
                    b.Property<string>("ID");

                    b.Property<string>("Description");

                    b.HasKey("ID");

                    b.ToTable("WorkingCostCentre");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Assignment", b =>
                {
                    b.HasOne("EniHRWebAPI.Models.Employee", "Employee")
                        .WithOne("Assignment")
                        .HasForeignKey("EniHRWebAPI.Models.Assignment", "EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Child", b =>
                {
                    b.HasOne("EniHRWebAPI.Models.Employee")
                        .WithMany("Children")
                        .HasForeignKey("EmployeeID");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.City", b =>
                {
                    b.HasOne("EniHRWebAPI.Models.Country", "CityCountry")
                        .WithMany()
                        .HasForeignKey("CityCountryID");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Employee", b =>
                {
                    b.HasOne("EniHRWebAPI.Models.Country", "CountryofBirth")
                        .WithMany()
                        .HasForeignKey("CountryofBirthID");

                    b.HasOne("EniHRWebAPI.Models.LocalPlus", "LocalPlus")
                        .WithMany()
                        .HasForeignKey("LocalPlusID");

                    b.HasOne("EniHRWebAPI.Models.Country", "Nationality")
                        .WithMany()
                        .HasForeignKey("NationalityID");

                    b.HasOne("EniHRWebAPI.Models.ReportsTo", "ReportsTo")
                        .WithMany()
                        .HasForeignKey("ReportsToID");

                    b.HasOne("EniHRWebAPI.Models.Country", "SpouseNationality")
                        .WithMany()
                        .HasForeignKey("SpouseNationalityID");

                    b.HasOne("EniHRWebAPI.Models.ActivityStatus", "activityStatus")
                        .WithMany()
                        .HasForeignKey("activityStatusID");

                    b.HasOne("EniHRWebAPI.Models.AssignmentStatus", "assignmentStatus")
                        .WithMany()
                        .HasForeignKey("assignmentStatusID");

                    b.HasOne("EniHRWebAPI.Models.BusinessUnit", "businessUnit")
                        .WithMany()
                        .HasForeignKey("businessUnitID");

                    b.HasOne("EniHRWebAPI.Models.City", "city")
                        .WithMany()
                        .HasForeignKey("cityID");

                    b.HasOne("EniHRWebAPI.Models.COS", "cos")
                        .WithMany()
                        .HasForeignKey("cosID");

                    b.HasOne("EniHRWebAPI.Models.FamilyStatus", "familyStatus")
                        .WithMany()
                        .HasForeignKey("familyStatusID");

                    b.HasOne("EniHRWebAPI.Models.HomeCompany", "homeCompany")
                        .WithMany()
                        .HasForeignKey("homeCompanyID");

                    b.HasOne("EniHRWebAPI.Models.Location", "location")
                        .WithMany()
                        .HasForeignKey("locationID");

                    b.HasOne("EniHRWebAPI.Models.OrganisationUnit", "organisationUnit")
                        .WithMany()
                        .HasForeignKey("organisationUnitID");

                    b.HasOne("EniHRWebAPI.Models.ParentPosition", "parentPosition")
                        .WithMany()
                        .HasForeignKey("parentPositionID");

                    b.HasOne("EniHRWebAPI.Models.Position", "position")
                        .WithMany()
                        .HasForeignKey("positionID");

                    b.HasOne("EniHRWebAPI.Models.ProfessionalArea", "professionalArea")
                        .WithMany()
                        .HasForeignKey("professionalAreaID");

                    b.HasOne("EniHRWebAPI.Models.Project", "project")
                        .WithMany()
                        .HasForeignKey("projectID");

                    b.HasOne("EniHRWebAPI.Models.StaffTypology", "staffTypology")
                        .WithMany()
                        .HasForeignKey("staffTypologyID");

                    b.HasOne("EniHRWebAPI.Models.StandardEmployeeCategory", "standardEmployeeCategory")
                        .WithMany()
                        .HasForeignKey("standardEmployeeCategoryID");

                    b.HasOne("EniHRWebAPI.Models.TypeOfVISA", "typeOfVISA")
                        .WithMany()
                        .HasForeignKey("typeOfVISAID");

                    b.HasOne("EniHRWebAPI.Models.WorkingCostCentre", "workingCostCentre")
                        .WithMany()
                        .HasForeignKey("workingCostCentreID");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Housing", b =>
                {
                    b.HasOne("EniHRWebAPI.Models.Employee", "employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID");
                });

            modelBuilder.Entity("EniHRWebAPI.Models.Leave", b =>
                {
                    b.HasOne("EniHRWebAPI.Models.Employee", "employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
