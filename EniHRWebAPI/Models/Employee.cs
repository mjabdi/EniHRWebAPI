using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EniHRWebAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EniHRWebAPI.Models
{
    public class Employee
    {

        [Display(Name = "Employee#")]
        public long EmployeeID { get; set; }

        public virtual Assignment Assignment { get; set; }

        public string Name { get ; set; }

        public string Surname { get; set; }

        public virtual LocalPlus LocalPlus { get; set; }

		public virtual StaffTypology staffTypology { get; set; }

		public virtual StandardEmployeeCategory standardEmployeeCategory { get; set; }

		public virtual Location location { get; set; }

		[Display(Name = "HRE.Code")]
		public long? HRECode { get; set; }

        public string Secretary { get; set; }

		public virtual BusinessUnit businessUnit { get; set; }

		[Display(Name = "Working.Company")]
		public string WorkingComapny { get; set; }

		public virtual OrganisationUnit organisationUnit { get; set; }

		public virtual WorkingCostCentre workingCostCentre { get; set; }

		public virtual Position position { get; set; }

        public virtual ParentPosition parentPosition { get; set; }

        public virtual COS cos { get; set; }

        public virtual Project project { get; set; }

        public virtual FamilyStatus familyStatus { get; set; }

        public virtual ActivityStatus activityStatus { get; set; }

        public virtual ProfessionalArea professionalArea { get; set; }

        public string SuperVisor { get; set; }

		[Display(Name = "Reports.To")]
        public ReportsTo ReportsTo { get; set; }


		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true,NullDisplayText = "N/A")]
		[Display(Name = "Company.Hiring.Date")]
		public DateTime? CompanyHiringDate { get; set; }

		public static explicit operator Employee(long v)
		{
			throw new NotImplementedException();
		}

		public virtual HomeCompany homeCompany { get; set; }

        [Display(Name = "No.of.Years.in.Eni")]
		[DisplayFormat(NullDisplayText = "N/A" , DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public double? NumberofYearsInEni
        {
            get
            {
                if (CompanyHiringDate == null)
                    return null;
                else
                {
					int days = ((TimeSpan)(DateTime.Today - CompanyHiringDate)).Days;
                    return ((double)days) / 365.0;
                }
            }
        }

        private GenderEnum Gender { get; set; }

       [Display(Name = "Gender")]
        public string GenderString { 
            get {
                switch (Gender)
                {
                    case GenderEnum.NotSet : 
                        return "N/A";
                    case GenderEnum.Female :
                        return "F";
                    case GenderEnum.Male:
                        return "M"; 
                    default :
                        return "N/A";
                }
            }
            set
            {
                switch (value)
                {
                    case "F" : 
                        Gender = GenderEnum.Female;
                        break;
                    case "M" :
                        Gender = GenderEnum.Male;
                        break;
                    default :
                        Gender = GenderEnum.NotSet;
                        break;
                }
              }
        }

		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
		[Display(Name = "Birth.Date")]
		public DateTime? BirthDate { get; set; }

        [Display(Name = "Age")]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public int? Age
        {
            get
            {
                if (BirthDate == null)
                    return null;
                else
                {
                    int days = ((TimeSpan)(DateTime.Today - BirthDate)).Days;
                    return (int) (days / 365.0);
                }
            }
        }

		public virtual Country CountryofBirth { get; set; }

		public virtual Country Nationality { get; set; }

		public virtual City city { get; set; }

        public string PointofOrigin
        {
            get
            {
                if (city != null)
                {
                    return city.CityName + ", " + CountryofBirth.CountryName;
                }
                else
                {
					if (CountryofBirth != null)
						return CountryofBirth.CountryName;
					else
						return "";
						
                }
            }
        }

        // return city name of origin
        public string SetPointofOriginAndRetunCity(string origin)
        {
            string[] parts =  origin.Split(",".ToCharArray());
            if (parts.Length == 2)
            {
                if (parts[1].Trim().ToUpper() == CountryofBirth.CountryName.Trim().ToUpper())
                {
                    return parts[0].Trim();
                }
            }
            return "";
        }

		public virtual AssignmentStatus assignmentStatus { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public bool? FollowingPartner { get; set; }

		[Display(Name = "Following.Partner")]
		public string FollowingPartnerString
		{
			get{
				if (FollowingPartner == null)
					return "N/A";
				else if ((bool)FollowingPartner)
				{
					return "Yes";
				}
				else
					return "No";
			}
		}

        public void SetFollowingPartnerString(string value)
		{
			switch (value)
            {
                case "Yes":
				case "True":	
                    FollowingPartner = true;
                    break;
                case "No":
				case "False":	
                    FollowingPartner = false;
                    break;
                default:
                    FollowingPartner = null;
                    break;
            }
		}

        [Display(Name = "Following.Children")]
        public int FollowingChildren { get; set; }

		public virtual Country SpouseNationality { get; set; }

        [Display(Name = "Spouse.Name")]
        public string SpouseName { get; set; }

		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
		[Display(Name = "Spouse.DOB")]
        public DateTime? SpouseDateofBirth { get; set; }

		public virtual List<Child> Children { get; set; }

		public virtual TypeOfVISA typeOfVISA { get; set; }

		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
		[Display(Name = "VISA.Expiray.Date")]
        public DateTime? VISAExpirayDate { get; set; }

        [Display(Name = "Email.Address")]
        public string EmailAddress { get; set; }

		[Display(Name = "Eni.Technical.ID")]
		public string UserId { get; set; }

        public void UpdateFromEmployeeViewModel(MyDBContext _context, EmployeeViewModel _employee)
        {
            try
            {

                if (this.EmployeeID != _employee.employeeID)
                {
                    throw new InvalidOperationException();
                }

                this.Name = _employee.name;
                this.Surname = _employee.surname;
                this.UserId = _employee.technicalID;

                LocalPlus _localPlus;
                try
                {
                    _localPlus = _context.LocalPlus.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.localPlus.Trim().ToLower());
                }
                catch (Exception)
                {
                    _localPlus = null;
                }

                if (_localPlus == null && !String.IsNullOrWhiteSpace(_employee.localPlus))
                {
                    _localPlus = new LocalPlus { Description = _employee.localPlus.Trim() };
                }
                this.LocalPlus = _localPlus;

                Location _location;
                try
                {
                    _location = _context.Location.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.workingLocation.Trim().ToLower());
                }
                catch (Exception)
                {
                    _location = null;
                }
                if (location == null && !String.IsNullOrWhiteSpace(_employee.workingLocation))
                {
                    _location = new Location { Description = _employee.workingLocation.Trim() };
                }
                this.location = _location;


                StandardEmployeeCategory _standardEmployeeCategory;
                try
                {
                    _standardEmployeeCategory = _context.StandardEmployeeCategory.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.employeeCategory.Trim().ToLower());
                }
                catch (Exception)
                {
                    _standardEmployeeCategory = null;
                }
                if (_standardEmployeeCategory == null && !String.IsNullOrWhiteSpace(_employee.employeeCategory))
                {
                    _standardEmployeeCategory = new StandardEmployeeCategory { Description = _employee.employeeCategory.Trim() };
                }
                this.standardEmployeeCategory = _standardEmployeeCategory;


                OrganisationUnit _organisationUnit;
                try
                {
                    _organisationUnit = _context.OrganisationUnit.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.organizationUnit.Trim().ToLower());
                }
                catch (InvalidOperationException)
                {
                    _organisationUnit = null;
                }
                if (_organisationUnit == null && !String.IsNullOrWhiteSpace(_employee.organizationUnit))
                {
                    _organisationUnit = new OrganisationUnit { Description = _employee.organizationUnit.Trim() };
                }
                this.organisationUnit = _organisationUnit;

                string[] cost_parts = _employee.costCentre?.Split("|".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
                if (cost_parts?.Length == 2)
                {
                    string costCode = cost_parts[0].Trim();
                    string costDesc = cost_parts[1].Trim();

                    WorkingCostCentre _workingCostCentre;
                    try
                    {
                        _workingCostCentre = _context.WorkingCostCentre.FirstOrDefault(l => l.ID == costCode);
                    }
                    catch (Exception)
                    {
                        _workingCostCentre = null;
                    }
                    if (_workingCostCentre == null)
                    {
                        _workingCostCentre = new WorkingCostCentre { ID = costCode, Description = costDesc.Trim() };
                        _context.WorkingCostCentre.Add(_workingCostCentre);
                    }
                    this.workingCostCentre = _workingCostCentre;
                }

                Position _position;
                try
                {
                    _position = _context.Position.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.position.Trim().ToLower());
                }
                catch (Exception)
                {
                    _position = null;
                }
                if (_position != null)
                {
                    this.position = _position;
                }
                else
                {
                    string[] posSplit = _employee.position.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (posSplit.Length == 2)
                    {
                        _position = _context.Position.FirstOrDefault(l => l.ID == posSplit[0].Trim());
                        if (_position == null)
                        {
                            _position = new Position { ID = posSplit[0].Trim(), Description = posSplit[1].Trim() };
                            _context.Position.Add(_position);
                        }

                        this.position = _position;
                    }
                }

                ProfessionalArea _professionalArea;
                try
                {
                    _professionalArea = _context.ProfessionalArea.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.professionalArea.Trim().ToLower());
                }
                catch (Exception)
                {
                    _professionalArea = null;
                }
                if (_professionalArea == null && !String.IsNullOrWhiteSpace(_employee.professionalArea))
                {
                    _professionalArea = new ProfessionalArea { Description = _employee.professionalArea.Trim() };
                }
                this.professionalArea = _professionalArea;

                this.CompanyHiringDate = _employee.companyHiringDate;

                HomeCompany _homeCompany;
                try
                {
                    _homeCompany = _context.HomeCompany.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.homeCompany.Trim().ToLower());
                }
                catch (Exception)
                {
                    _homeCompany = null;
                }
                if (_homeCompany == null && !String.IsNullOrWhiteSpace(_employee.homeCompany))
                {
                    _homeCompany = new HomeCompany { Description = _employee.homeCompany.Trim() };
                }
                this.homeCompany = _homeCompany;


                this.GenderString = _employee.gender;

                this.BirthDate = _employee.birthDate;

                Country _countryofBirth = null;
                try
                {
                    _countryofBirth = _context.Country.FirstOrDefault(l => l.CountryName.Trim().ToLower() == _employee.countryOfBirth.Trim().ToLower());
                }
                catch (Exception)
                {
                    _countryofBirth = null;
                }
                if (_countryofBirth == null && !String.IsNullOrWhiteSpace(_employee.countryOfBirth))
                {
                    _countryofBirth = new Country { CountryName = _employee.countryOfBirth.Trim() };
                }
                this.CountryofBirth = _countryofBirth;

                Country _nationality = null;
                try
                {
                    _nationality = _context.Country.FirstOrDefault(l => l.CountryName.Trim().ToLower() == _employee.nationality.Trim().ToLower());
                }
                catch (Exception)
                {
                    _nationality = null;
                }
                if (_nationality == null && !String.IsNullOrWhiteSpace(_employee.nationality))
                {
                    _nationality = new Country { CountryName = _employee.nationality.Trim() };
                }
                this.Nationality = _nationality;


                Country _spousenationality = null;
                try
                {
                    _spousenationality = _context.Country.FirstOrDefault(l => l.CountryName.Trim().ToLower() == _employee.spouseNationality.Trim().ToLower());
                }
                catch (Exception)
                {
                    _spousenationality = null;
                }
                if (_spousenationality == null && !String.IsNullOrWhiteSpace(_employee.spouseNationality))
                {
                    _spousenationality = new Country { CountryName = _employee.spouseNationality.Trim() };
                }
                this.SpouseNationality = _spousenationality;

                FamilyStatus _familyStatus;
                try
                {
                    _familyStatus = _context.FamilyStatus.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.familyStatus.Trim().ToLower());
                }
                catch (Exception)
                {
                    _familyStatus = null;
                }
                if (_familyStatus == null && !String.IsNullOrWhiteSpace(_employee.familyStatus))
                {
                    _familyStatus = new FamilyStatus { Description = _employee.familyStatus.Trim() };
                }
                this.familyStatus = _familyStatus;

                this.FollowingPartner = _employee.followingPartner;

                this.FollowingChildren = _employee.followingChildren.HasValue ? _employee.followingChildren.Value : 0;

                TypeOfVISA _typeOfVISA;
                try
                {
                    _typeOfVISA = _context.TypeOfVISA.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.typeOfVisa.Trim().ToLower());
                }
                catch (Exception)
                {
                    _typeOfVISA = null;
                }
                if (_typeOfVISA == null && !String.IsNullOrWhiteSpace(_employee.typeOfVisa))
                {
                    _typeOfVISA = new TypeOfVISA { Description = _employee.typeOfVisa.Trim() };
                }
                this.typeOfVISA = _typeOfVISA;

                this.VISAExpirayDate = _employee.visaExpiryDate;
                this.EmailAddress = _employee.emailAddress;

                ActivityStatus _activityStatus;
                try
                {
                    _activityStatus = _context.ActivityStatus.FirstOrDefault(l => l.Description.Trim().ToLower() == _employee.activityStatus.Trim().ToLower());
                }
                catch (Exception)
                {
                    _activityStatus = null;
                }
                if (_activityStatus == null && !String.IsNullOrWhiteSpace(_employee.activityStatus))
                {
                    _activityStatus = new ActivityStatus { Description = _employee.activityStatus.Trim() };
                }
                this.activityStatus = _activityStatus;
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }




    public enum GenderEnum : int
    {
        NotSet = 0,
        Female = 1,
        Male = 2
    }
}