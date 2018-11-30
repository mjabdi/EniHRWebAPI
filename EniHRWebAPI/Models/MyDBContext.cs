using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace EniHRWebAPI.Models
{
    public class MyDBContext : DbContext
    {

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) 
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<Housing> Housing { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Assignment> Assignments { get; set; }

        public virtual DbSet<Leave> Leaves { get; set; }

        public virtual DbSet<ICT> ICTs { get; set; }


        public virtual DbSet<LocalPlus> LocalPlus { get; set; }
        public virtual DbSet<StaffTypology> StaffTypology { get; set; }
        public virtual DbSet<StandardEmployeeCategory> StandardEmployeeCategory { get; set; }
        public virtual DbSet<BusinessUnit> BusinessUnit { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<OrganisationUnit> OrganisationUnit { get; set; }
        public virtual DbSet<WorkingCostCentre> WorkingCostCentre { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<ParentPosition> ParentPosition { get; set; }
        public virtual DbSet<COS> COS { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<FamilyStatus> FamilyStatus { get; set; }
        public virtual DbSet<ActivityStatus> ActivityStatus { get; set; }
        public virtual DbSet<ReportsTo> ReportsTo { get; set; }

        public virtual DbSet<ProfessionalArea> ProfessionalArea { get; set; }
        public virtual DbSet<HomeCompany> HomeCompany { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<AssignmentStatus> AssignmentStatus { get; set; }
        public virtual DbSet<TypeOfVISA> TypeOfVISA { get; set; }
        public virtual DbSet<Child> Child { get; set; }


    }
}
