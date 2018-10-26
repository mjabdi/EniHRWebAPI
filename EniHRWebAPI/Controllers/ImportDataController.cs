using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EniHRWebAPI.ViewModel;
using EniHRWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EniHRWebAPI.Controllers
{
    [Route("api/importdata")]
    public class ImportDataController : Controller
    {

        private readonly MyDBContext context;

        private readonly decimal DIFFERENCEAMOUNT = 1.0M;

        public ImportDataController(MyDBContext _context)
        {
            this.context = _context;
        }

        [HttpPost("validatedata")]
        public async Task<List<ValidationResponse>> ValidateDataAsync([FromBody]object[][] data)
        {
            try
            {

                if (data[0].Length != 71)
                {
                    throw new Exception("Wrong Columns!");
                }

                if (data[0][0].ToString().ToLower() != "employee number")
                    throw new Exception("Wrong Columns!");

                if (data[0][1].ToString().ToLower() != "last name")
                    throw new Exception("Wrong Columns!");

                if (data[0][2].ToString().ToLower() != "name")
                    throw new Exception("Wrong Columns!");

                if (data[0][70].ToString().ToLower() != "current  rent")
                    throw new Exception("Wrong Columns!");

                List<ValidationResponse> response = new List<ValidationResponse>();

                List<Employee> employeeList = await context.Employees
                    .Include(e => e.LocalPlus)
                    .Include(e => e.standardEmployeeCategory)
                    .Include(e => e.location)
                    .Include(e => e.businessUnit)
                    .Include(e => e.organisationUnit)
                    .Include(e => e.workingCostCentre)
                    .Include(e => e.position)
                    .Include(e => e.professionalArea)
                    .Include(e => e.homeCompany)
                    .Include(e => e.CountryofBirth)
                    .Include(e => e.Nationality)
                    .Include(e => e.SpouseNationality)
                    .Include(e => e.city)
                    .Include(e => e.typeOfVISA)
                    .Include(e => e.assignmentStatus)
                    .Include(e => e.Children)
                    .Include(e => e.familyStatus)
                    .Include(e => e.activityStatus)
                    //.Where(e => e.activityStatus.Description.ToLower().Trim() != "leaver")
                                                           .ToListAsync();

                Dictionary<long, Employee> employeeDic = new Dictionary<long, Employee>();
                foreach (Employee emp in employeeList)
                {
                    employeeDic.Add(emp.EmployeeID, emp);
                }


                List<Housing> housingList = await context.Housing.ToListAsync();
                Dictionary<long, Housing> housingDic = new Dictionary<long, Housing>();
                foreach(Housing house in housingList)
                {
                    housingDic.Add(house.HousingID, house);
                }




                for (int i = 1; i < data.Length; i++)
                {
                    try
                    {

                        ValidationResponse res = new ValidationResponse();
                        res.rowIndex = i;
                        res.changedColumns = new List<ChangeColumn>();

                        var employeeNo = long.Parse(data[i][0].ToString().Trim());

                        var name = "";
                        try
                        {
                            name = data[i][2]?.ToString().Trim();
                        }
                        catch(Exception){}

                        var surname = "";
                        try
                        {
                            surname = data[i][1]?.ToString().Trim();
                        }
                        catch(Exception){}

                        var technicalID = "";
                        try
                        {
                            technicalID = data[i][6]?.ToString().Trim();
                        }catch(Exception){}

                        var workingLocation = "";
                        try
                        {
                            workingLocation = data[i][10]?.ToString().Trim();
                        }catch(Exception){}

                        var employeeCat = "";
                        try
                        {
                            employeeCat = data[i][5]?.ToString().Trim();
                        }catch(Exception){}

                        var organizationUnit = "";
                        try
                        {
                            organizationUnit =data[i][13]?.ToString().Trim();
                        }catch(Exception){}


                        var costCenterCode = "";
                        try
                        {
                            costCenterCode = data[i][8]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var costCenterDesc = "";
                        try
                        {
                            costCenterDesc = data[i][9]?.ToString().Trim();
                        }
                        catch (Exception) { }



                        var positionStr = "";
                        var positionCode = "";
                        var positionDesc = "";
                        try
                        {
                            positionStr = data[i][11]?.ToString().Trim();
                            try
                            {
                                string[] splitPosition = positionStr.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                positionCode = splitPosition[1].Trim();
                                positionDesc = splitPosition[2].Trim();
                            }
                            catch (Exception) { }
                        }catch(Exception){}

                        var professionalArea = "";
                        try
                        {
                            professionalArea = data[i][12]?.ToString().Trim();
                        }catch(Exception){}

                        var homeCompany = "";
                        try
                        {
                            homeCompany = data[i][21]?.ToString().Trim();
                        }catch(Exception){}

                        var gender = "";
                        try
                        {
                            gender = data[i][16]?.ToString().Trim();
                        }catch(Exception){}

                        var countryofBirth = "";
                        try
                        {
                            countryofBirth = data[i][18]?.ToString().Trim();
                        }catch(Exception){}

                        var nationality = "";
                        try
                        {
                            nationality = data[i][19]?.ToString().Trim();
                        }catch(Exception){}

                        var familyStatus = "";
                        try
                        {
                            familyStatus = data[i][22]?.ToString().Trim();
                        }catch(Exception){}

                        bool? followingPartner = null;
                        try
                        {
                            followingPartner = data[i][23]?.ToString().Trim().ToLower() == "y" ? true : false;
                        }catch(Exception){}

                        int? followingChildren = null;
                        try
                        {
                            followingChildren = int.Parse(data[i][24]?.ToString());
                        }catch(Exception){}

                        var emailAddress = "";
                        try
                        {
                            emailAddress = data[i][7]?.ToString().ToLower().Trim();
                        }catch(Exception){}

                        var homeAddress = "";
                        try
                        {
                            homeAddress = data[i][30]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) {}

                        decimal? unfurnishedWeek = null;
                        try
                        {
                            unfurnishedWeek = decimal.Parse(data[i][31].ToString());
                        }
                        catch (Exception) { }



                        decimal? diffAllowanceMonthlyPaid = null;
                        try
                        {
                            diffAllowanceMonthlyPaid = decimal.Parse(data[i][35].ToString());
                        }
                        catch (Exception) { }

                        decimal? furnitureAllowance = null;
                        try
                        {
                            furnitureAllowance = decimal.Parse(data[i][36].ToString());
                        }
                        catch (Exception) { }

                        decimal? actualFurnitureCosts = null;
                        try
                        {
                            actualFurnitureCosts = decimal.Parse(data[i][37].ToString());
                        }
                        catch (Exception) { }

                        decimal? parkingCharges = null;
                        try
                        {
                            parkingCharges = decimal.Parse(data[i][38].ToString());
                        }
                        catch (Exception) { }

                        decimal? regularPayrollDeduction = null;
                        try
                        {
                            regularPayrollDeduction = decimal.Parse(data[i][39].ToString());
                        }
                        catch (Exception) { }

                        var utilitiesIncuded = "";
                        try
                        {
                            utilitiesIncuded = data[i][40]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }

                        var furnishedUnfurnished = "";
                        try
                        {
                            furnishedUnfurnished = data[i][41]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }



                        var housingComments = "";
                        try
                        {
                            housingComments = data[i][42]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }

                        //new changes ****************************


                        int? entitledBedrroms = null;
                        try
                        {
                            entitledBedrroms = int.Parse(data[i][61]?.ToString().Trim());
                        }catch(Exception){}

                        int? actualBedrroms = null;
                        try
                        {
                            actualBedrroms = int.Parse(data[i][62]?.ToString().Trim());
                        }
                        catch (Exception) { }

                        string typeofProperty = "";
                        try
                        {
                            typeofProperty = data[i][63]?.ToString().ToLower().Trim();
                            if (typeofProperty.Length <= 1)
                                typeofProperty = "";
                        }
                        catch (Exception) { }

                        int? rentDueDate = null;
                        try
                        {
                            rentDueDate = int.Parse(data[i][64]?.ToString().Trim());
                        }
                        catch (Exception) { }


                        DateTime? tenancyStartDate = null;
                        try
                        {
                            var dateStr = data[i][65]?.ToString().ToLower().Trim();
                            tenancyStartDate = DateTime.FromOADate(double.Parse(dateStr));
                        }
                        catch(Exception){}

                        DateTime? tenancyEndDate = null;
                        try
                        {
                            var dateStr = data[i][66]?.ToString().ToLower().Trim();
                            tenancyEndDate = DateTime.FromOADate(double.Parse(dateStr));
                        }
                        catch (Exception) { }

                        string monthNoticePeriod = "";
                        try
                        {
                            monthNoticePeriod = data[i][68]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }

                        decimal? initialRent = null;
                        try
                        {
                            initialRent = decimal.Parse(data[i][69].ToString());
                        }catch (Exception) { }

                        decimal? currentRent = null;
                        try
                        {
                            currentRent = decimal.Parse(data[i][70].ToString());
                        }catch (Exception) { }


                        DateTime? companyHiringDate = null;
                        try
                        {
                            var dateStr = data[i][20]?.ToString().ToLower().Trim();
                            companyHiringDate = DateTime.FromOADate(double.Parse(dateStr));
                        }
                        catch (Exception) { }

                        DateTime? birthDate = null;
                        try
                        {
                            var dateStr = data[i][17]?.ToString().ToLower().Trim();
                            birthDate = DateTime.FromOADate(double.Parse(dateStr));
                        }
                        catch (Exception) { }

                        //    Employee employee = await context.Employees
                        //.Include(e => e.LocalPlus)
                        //.Include(e => e.standardEmployeeCategory)
                        //.Include(e => e.location)
                        //.Include(e => e.businessUnit)
                        //.Include(e => e.organisationUnit)
                        //.Include(e => e.workingCostCentre)
                        //.Include(e => e.position)
                        //.Include(e => e.professionalArea)
                        //.Include(e => e.homeCompany)
                        //.Include(e => e.CountryofBirth)
                        //.Include(e => e.Nationality)
                        //.Include(e => e.SpouseNationality)
                        //.Include(e => e.city)
                        //.Include(e => e.typeOfVISA)
                        //.Include(e => e.assignmentStatus)
                        //.Include(e => e.Children)
                        //.Include(e => e.familyStatus)
                        //.Include(e => e.activityStatus)
                        ////.Where(e => e.activityStatus.Description.ToLower().Trim() != "leaver")
                        //.FirstOrDefaultAsync(e => e.EmployeeID == employeeNo);

                        Employee employee = employeeDic[employeeNo];

                        if (employee == null)
                        {
                            ////**** New Employee Found  *****
                            res.isNew = true;
                            response.Add(res);
                        }
                        else
                        {
                            //Housing housing = context.Housing.Find(employeeNo);

                            Housing housing = housingDic[employeeNo];

                            if (!string.IsNullOrEmpty(name) && employee.Name.ToLower() != name.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 2;
                                cc.currentValue = employee.Name;
                                cc.newValue = name;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(surname) && employee.Surname.ToLower() != surname.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 1;
                                cc.currentValue = employee.Surname;
                                cc.newValue = surname;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(technicalID) && !string.IsNullOrEmpty(employee.UserId) && employee.UserId.ToLower() != technicalID.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 6;
                                cc.currentValue = employee.UserId;
                                cc.newValue = technicalID;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(workingLocation) && employee.location?.Description.Trim().ToLower() != workingLocation.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 10;
                                cc.currentValue = employee.location?.Description;
                                cc.newValue = workingLocation;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(employeeCat) && employee.standardEmployeeCategory?.Description.Trim().ToLower() != employeeCat.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 5;
                                cc.currentValue = employee.standardEmployeeCategory?.Description;
                                cc.newValue = employeeCat;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(organizationUnit) && employee.organisationUnit?.Description.Trim().ToLower() != organizationUnit.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 13;
                                cc.currentValue = employee.organisationUnit?.Description;
                                cc.newValue = organizationUnit;
                                res.changedColumns.Add(cc);
                            }

                            if (!string.IsNullOrEmpty(costCenterCode) && !string.IsNullOrEmpty(costCenterDesc)  && employee.workingCostCentre?.ID.Trim().ToLower() != costCenterCode.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 8;
                                cc.currentValue = employee.workingCostCentre?.ID;
                                cc.newValue = costCenterCode;
                                res.changedColumns.Add(cc);
                            }

                            //if (!string.IsNullOrEmpty(costCenterDesc) && employee.workingCostCentre?.Description.Trim().ToLower() != costCenterDesc.ToLower())
                            //{
                            //    ChangeColumn cc = new ChangeColumn();
                            //    cc.colIndex = 9;
                            //    cc.currentValue = employee.workingCostCentre?.Description;
                            //    cc.newValue = costCenterDesc;
                            //    res.changedColumns.Add(cc);
                            //}


                            if (!string.IsNullOrEmpty(positionDesc) && !string.IsNullOrEmpty(positionCode) && employee.position?.ID.ToLower() != positionCode.ToLower() &&  employee.position?.Description.ToLower() != positionDesc.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 11;
                                cc.currentValue = "E|" + employee.position?.ID + "|" + employee.position?.Description;
                                cc.newValue = positionStr;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(professionalArea) && employee.professionalArea?.Description.Trim().ToLower() != professionalArea.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 12;
                                cc.currentValue = employee.professionalArea?.Description;
                                cc.newValue = professionalArea;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(homeCompany) && employee.homeCompany?.Description.Trim().ToLower() != homeCompany.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 21;
                                cc.currentValue = employee.homeCompany?.Description;
                                cc.newValue = homeCompany;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(gender) && employee.GenderString.ToLower() != gender.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 16;
                                cc.currentValue = employee.GenderString;
                                cc.newValue = gender;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(countryofBirth) && employee.CountryofBirth?.CountryName.Trim().ToLower() != countryofBirth.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 18;
                                cc.currentValue = employee.CountryofBirth?.CountryName;
                                cc.newValue = countryofBirth;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(nationality) && employee.Nationality?.CountryName.Trim().ToLower() != nationality.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 19;
                                cc.currentValue = employee.Nationality?.CountryName;
                                cc.newValue = nationality;
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(familyStatus) && employee.familyStatus?.Description.Trim().ToLower() != familyStatus.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 22;
                                cc.currentValue = employee.familyStatus?.Description;
                                cc.newValue = familyStatus;
                                res.changedColumns.Add(cc);
                            }
                            if (followingPartner.HasValue && employee.FollowingPartner.HasValue && employee.FollowingPartner.Value != followingPartner.Value)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 23;
                                cc.currentValue = employee.FollowingPartner.Value ? "Y" : "N";
                                cc.newValue = followingPartner.Value ? "Y" : "N";
                                res.changedColumns.Add(cc);
                            }
                            else if (followingPartner.HasValue && !employee.FollowingPartner.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 23;
                                cc.currentValue = "N/A";
                                cc.newValue = followingPartner.Value ? "Y" : "N";
                                res.changedColumns.Add(cc);
                            }
                            if (followingChildren.HasValue && employee.FollowingChildren != followingChildren.Value)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 24;
                                cc.currentValue = employee.FollowingChildren.ToString();
                                cc.newValue = followingChildren.Value.ToString();
                                res.changedColumns.Add(cc);
                            }
                            if (!string.IsNullOrEmpty(emailAddress) && employee.EmailAddress?.Trim().ToLower() != emailAddress.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 7;
                                cc.currentValue = employee.EmailAddress;
                                cc.newValue = emailAddress;
                                res.changedColumns.Add(cc);
                            }

                            if (!string.IsNullOrEmpty(homeAddress) && housing.HomeAddressUK?.Trim().ToLower() != homeAddress.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 30;
                                cc.currentValue = housing.HomeAddressUK;
                                cc.newValue = homeAddress;
                                res.changedColumns.Add(cc);
                            }

                            if (!string.IsNullOrEmpty(housingComments) && housing.HousingComments?.Trim().ToLower() != housingComments.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 42;
                                cc.currentValue = housing.HousingComments;
                                cc.newValue = housingComments;
                                res.changedColumns.Add(cc);
                            }

                            if (unfurnishedWeek.HasValue && housing.EntitledUnFurnishedAllowanceWeek.HasValue && Math.Abs(housing.EntitledUnFurnishedAllowanceWeek.Value - unfurnishedWeek.Value).CompareTo(DIFFERENCEAMOUNT) > 0 )
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 31;
                                cc.currentValue = housing.EntitledUnFurnishedAllowanceWeek.ToString();
                                cc.newValue = unfurnishedWeek.ToString();
                                res.changedColumns.Add(cc);
                            }else if (unfurnishedWeek.HasValue && !housing.EntitledUnFurnishedAllowanceWeek.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 31;
                                cc.currentValue = "";
                                cc.newValue = unfurnishedWeek.ToString();
                                res.changedColumns.Add(cc); 
                            }

                            if (diffAllowanceMonthlyPaid.HasValue && housing.DifferenceAllowanceMonthlyCostsPaid.HasValue && Math.Abs(housing.DifferenceAllowanceMonthlyCostsPaid.Value - diffAllowanceMonthlyPaid.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 35;
                                cc.currentValue = housing.DifferenceAllowanceMonthlyCostsPaid.ToString();
                                cc.newValue = diffAllowanceMonthlyPaid.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (diffAllowanceMonthlyPaid.HasValue && !housing.DifferenceAllowanceMonthlyCostsPaid.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 35;
                                cc.currentValue = "";
                                cc.newValue = diffAllowanceMonthlyPaid.ToString();
                                res.changedColumns.Add(cc);
                            }

                            if (furnitureAllowance.HasValue && housing.FurnitureAllowance.HasValue && Math.Abs(housing.FurnitureAllowance.Value - furnitureAllowance.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 36;
                                cc.currentValue = housing.FurnitureAllowance.ToString();
                                cc.newValue = furnitureAllowance.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (furnitureAllowance.HasValue && !housing.FurnitureAllowance.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 36;
                                cc.currentValue = "";
                                cc.newValue = furnitureAllowance.ToString();
                                res.changedColumns.Add(cc);
                            }

                            if (actualFurnitureCosts.HasValue && housing.ActualFurnitureCosts.HasValue && Math.Abs(housing.ActualFurnitureCosts.Value - actualFurnitureCosts.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 37;
                                cc.currentValue = housing.ActualFurnitureCosts.ToString();
                                cc.newValue = actualFurnitureCosts.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (actualFurnitureCosts.HasValue && !housing.ActualFurnitureCosts.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 37;
                                cc.currentValue = "";
                                cc.newValue = actualFurnitureCosts.ToString();
                                res.changedColumns.Add(cc);
                            }

                            if (parkingCharges.HasValue && housing.ParkingCharges.HasValue && Math.Abs(housing.ParkingCharges.Value - parkingCharges.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 38;
                                cc.currentValue = housing.ParkingCharges.ToString();
                                cc.newValue = parkingCharges.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (parkingCharges.HasValue && !housing.ParkingCharges.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 38;
                                cc.currentValue = "";
                                cc.newValue = parkingCharges.ToString();
                                res.changedColumns.Add(cc);
                            }

                            if (regularPayrollDeduction.HasValue && housing.RegularPayrollDeduction.HasValue && Math.Abs(housing.RegularPayrollDeduction.Value - regularPayrollDeduction.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 39;
                                cc.currentValue = housing.RegularPayrollDeduction.ToString();
                                cc.newValue = regularPayrollDeduction.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (regularPayrollDeduction.HasValue && !housing.RegularPayrollDeduction.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 39;
                                cc.currentValue = "";
                                cc.newValue = regularPayrollDeduction.ToString();
                                res.changedColumns.Add(cc);
                            }

                            if (!string.IsNullOrEmpty(utilitiesIncuded) && housing.UtilitiesIncluded?.Trim().ToLower() != utilitiesIncuded.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 40;
                                cc.currentValue = housing.UtilitiesIncluded;
                                cc.newValue = utilitiesIncuded;
                                res.changedColumns.Add(cc);
                            }

                            if (!string.IsNullOrEmpty(furnishedUnfurnished) && housing.FurnishedUnFurnished?.Trim().ToLower() != furnishedUnfurnished.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 41;
                                cc.currentValue = housing.FurnishedUnFurnished;
                                cc.newValue = furnishedUnfurnished;
                                res.changedColumns.Add(cc);
                            }



                            if (entitledBedrroms.HasValue && housing.EntitledNumberofBedrooms.HasValue && entitledBedrroms.Value !=housing.EntitledNumberofBedrooms.Value)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 61;
                                cc.currentValue = housing.EntitledNumberofBedrooms.Value.ToString();
                                cc.newValue = entitledBedrroms.Value.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (entitledBedrroms.HasValue && !housing.EntitledNumberofBedrooms.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 61;
                                cc.currentValue = "";
                                cc.newValue = entitledBedrroms.Value.ToString();
                                res.changedColumns.Add(cc);
                            }


                            if (actualBedrroms.HasValue && housing.ActualNumberofBedrooms.HasValue && actualBedrroms.Value != housing.ActualNumberofBedrooms.Value)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 62;
                                cc.currentValue = housing.ActualNumberofBedrooms.Value.ToString();
                                cc.newValue = actualBedrroms.Value.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (actualBedrroms.HasValue && !housing.ActualNumberofBedrooms.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 62;
                                cc.currentValue = "";
                                cc.newValue = actualBedrroms.Value.ToString();
                                res.changedColumns.Add(cc);
                            }

                            if (!string.IsNullOrEmpty(typeofProperty) && housing.TypeOfProperty?.Trim().ToLower() != typeofProperty.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 63;
                                cc.currentValue = housing.TypeOfProperty;
                                cc.newValue = typeofProperty;
                                res.changedColumns.Add(cc);
                            }

                            if (rentDueDate.HasValue && housing.RentDueDate.HasValue && rentDueDate.Value != housing.RentDueDate.Value)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 64;
                                cc.currentValue = housing.RentDueDate.Value.ToString();
                                cc.newValue = rentDueDate.Value.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (rentDueDate.HasValue && !housing.RentDueDate.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 64;
                                cc.currentValue = "";
                                cc.newValue = rentDueDate.Value.ToString();
                                res.changedColumns.Add(cc);
                            }

                            if (tenancyStartDate.HasValue && housing.TenancyAgreementStartDate.HasValue && tenancyStartDate.Value.Date != housing.TenancyAgreementStartDate.Value.Date)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 65;
                                cc.currentValue = housing.TenancyAgreementStartDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                cc.newValue = tenancyStartDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                res.changedColumns.Add(cc);
                            }
                            else if (tenancyStartDate.HasValue && !housing.TenancyAgreementStartDate.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 65;
                                cc.currentValue = "";
                                cc.newValue = tenancyStartDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                res.changedColumns.Add(cc);
                            }

                            if (tenancyEndDate.HasValue && housing.TenancyAgreementEndDate.HasValue && tenancyEndDate.Value.Date != housing.TenancyAgreementEndDate.Value.Date)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 66;
                                cc.currentValue = housing.TenancyAgreementEndDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                cc.newValue = tenancyEndDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                res.changedColumns.Add(cc);
                            }
                            else if (tenancyEndDate.HasValue && !housing.TenancyAgreementEndDate.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 66;
                                cc.currentValue = "";
                                cc.newValue = tenancyEndDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                res.changedColumns.Add(cc);
                            }


                            if (!string.IsNullOrEmpty(monthNoticePeriod) && housing.MonthNoticePeriod?.Trim().ToLower() != monthNoticePeriod.ToLower())
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 68;
                                cc.currentValue = housing.MonthNoticePeriod;
                                cc.newValue = monthNoticePeriod;
                                res.changedColumns.Add(cc);
                            }

                            if (initialRent.HasValue && housing.InitialHouseContractRent.HasValue && Math.Abs(housing.InitialHouseContractRent.Value - initialRent.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 69;
                                cc.currentValue = housing.InitialHouseContractRent.ToString();
                                cc.newValue = initialRent.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (initialRent.HasValue && !housing.InitialHouseContractRent.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 69;
                                cc.currentValue = "";
                                cc.newValue = initialRent.ToString();
                                res.changedColumns.Add(cc);
                            }

                            if (currentRent.HasValue && housing.CurrentHouseRental.HasValue && Math.Abs(housing.CurrentHouseRental.Value - currentRent.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 70;
                                cc.currentValue = housing.CurrentHouseRental.ToString();
                                cc.newValue = currentRent.ToString();
                                res.changedColumns.Add(cc);
                            }
                            else if (currentRent.HasValue && !housing.CurrentHouseRental.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 70;
                                cc.currentValue = "";
                                cc.newValue = currentRent.ToString();
                                res.changedColumns.Add(cc);
                            }


                            if (companyHiringDate.HasValue && employee.CompanyHiringDate.HasValue && companyHiringDate.Value.Date != employee.CompanyHiringDate.Value.Date)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 20;
                                cc.currentValue = employee.CompanyHiringDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                cc.newValue = companyHiringDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                res.changedColumns.Add(cc);
                            }
                            else if (companyHiringDate.HasValue && !employee.CompanyHiringDate.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 20;
                                cc.currentValue = "";
                                cc.newValue = companyHiringDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                res.changedColumns.Add(cc);
                            }


                            if (birthDate.HasValue && employee.BirthDate.HasValue && birthDate.Value.Date != employee.BirthDate.Value.Date)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 17;
                                cc.currentValue = employee.BirthDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                cc.newValue = birthDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                res.changedColumns.Add(cc);
                            }
                            else if (birthDate.HasValue && !employee.BirthDate.HasValue)
                            {
                                ChangeColumn cc = new ChangeColumn();
                                cc.colIndex = 17;
                                cc.currentValue = "";
                                cc.newValue = birthDate.Value.Date.ToString("dd'/'MM'/'yyyy");
                                res.changedColumns.Add(cc);
                            }

                            res.isChanged = res.changedColumns.Count > 0;
                            response.Add(res);
                        }
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        [HttpPost("applychanges")]
        public async Task<ActionResult> ApplyChanges([FromBody]object[][] data)
        {
            try
            {

                     List<Employee> employeeList = await context.Employees
                    .Include(e => e.LocalPlus)
                    .Include(e => e.standardEmployeeCategory)
                    .Include(e => e.location)
                    .Include(e => e.businessUnit)
                    .Include(e => e.organisationUnit)
                    .Include(e => e.workingCostCentre)
                    .Include(e => e.position)
                    .Include(e => e.professionalArea)
                    .Include(e => e.homeCompany)
                    .Include(e => e.CountryofBirth)
                    .Include(e => e.Nationality)
                    .Include(e => e.SpouseNationality)
                    .Include(e => e.city)
                    .Include(e => e.typeOfVISA)
                    .Include(e => e.assignmentStatus)
                    .Include(e => e.Children)
                    .Include(e => e.familyStatus)
                    .Include(e => e.activityStatus)
                    //.Where(e => e.activityStatus.Description.ToLower().Trim() != "leaver")
                                                           .ToListAsync();

                Dictionary<long, Employee> employeeDic = new Dictionary<long, Employee>();
                foreach (Employee emp in employeeList)
                {
                    employeeDic.Add(emp.EmployeeID, emp);
                }


                List<Housing> housingList = await context.Housing.ToListAsync();
                Dictionary<long, Housing> housingDic = new Dictionary<long, Housing>();
                foreach(Housing house in housingList)
                {
                    housingDic.Add(house.HousingID, house);
                }





                for (int i = 0; i < data.Length; i++)
                {
                    try
                    {

                        var employeeNo = long.Parse(data[i][0].ToString().Trim());

                        var name = "";
                        try
                        {
                            name = data[i][2]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var surname = "";
                        try
                        {
                            surname = data[i][1]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var technicalID = "";
                        try
                        {
                            technicalID = data[i][6]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var workingLocation = "";
                        try
                        {
                            workingLocation = data[i][10]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var employeeCat = "";
                        try
                        {
                            employeeCat = data[i][5]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var organizationUnit = "";
                        try
                        {
                            organizationUnit = data[i][13]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var costCenterCode = "";
                        try
                        {
                            costCenterCode = data[i][8]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var costCenterDesc = "";
                        try
                        {
                            costCenterDesc = data[i][9]?.ToString().Trim();
                        }
                        catch (Exception) { }



                        var positionStr = "";
                        var positionCode = "";
                        var positionDesc = "";
                        try
                        {
                            positionStr = data[i][11]?.ToString().Trim();
                            try
                            {
                                string[] splitPosition = positionStr.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                positionCode = splitPosition[1].Trim();
                                positionDesc = splitPosition[2].Trim();
                            }
                            catch (Exception) { }
                        }
                        catch (Exception) { }

                        var professionalArea = "";
                        try
                        {
                            professionalArea = data[i][12]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var homeCompany = "";
                        try
                        {
                            homeCompany = data[i][21]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var gender = "";
                        try
                        {
                            gender = data[i][16]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var countryofBirth = "";
                        try
                        {
                            countryofBirth = data[i][18]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var nationality = "";
                        try
                        {
                            nationality = data[i][19]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        var familyStatus = "";
                        try
                        {
                            familyStatus = data[i][22]?.ToString().Trim();
                        }
                        catch (Exception) { }

                        bool? followingPartner = null;
                        try
                        {
                            followingPartner = data[i][23]?.ToString().Trim().ToLower() == "y" ? true : false;
                        }
                        catch (Exception) { }

                        int? followingChildren = null;
                        try
                        {
                            followingChildren = int.Parse(data[i][24]?.ToString());
                        }
                        catch (Exception) { }

                        var emailAddress = "";
                        try
                        {
                            emailAddress = data[i][7]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }

                        var homeAddress = "";
                        try
                        {
                            homeAddress = data[i][30]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }

                        decimal? unfurnishedWeek = null;
                        try
                        {
                            unfurnishedWeek = decimal.Parse(data[i][31].ToString());
                        }
                        catch (Exception) { }


                        decimal? diffAllowanceMonthlyPaid = null;
                        try
                        {
                            diffAllowanceMonthlyPaid = decimal.Parse(data[i][35].ToString());
                        }
                        catch (Exception) { }

                        decimal? furnitureAllowance = null;
                        try
                        {
                            furnitureAllowance = decimal.Parse(data[i][36].ToString());
                        }
                        catch (Exception) { }

                        decimal? actualFurnitureCosts = null;
                        try
                        {
                            actualFurnitureCosts = decimal.Parse(data[i][37].ToString());
                        }
                        catch (Exception) { }

                        decimal? parkingCharges = null;
                        try
                        {
                            parkingCharges = decimal.Parse(data[i][38].ToString());
                        }
                        catch (Exception) { }

                        decimal? regularPayrollDeduction = null;
                        try
                        {
                            regularPayrollDeduction = decimal.Parse(data[i][39].ToString());
                        }
                        catch (Exception) { }

                        var utilitiesIncuded = "";
                        try
                        {
                            utilitiesIncuded = data[i][40]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }

                        var furnishedUnfurnished = "";
                        try
                        {
                            furnishedUnfurnished = data[i][41]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }




                        var housingComments = "";
                        try
                        {
                            housingComments = data[i][42]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }



                        //new changes ****************************


                        int? entitledBedrroms = null;
                        try
                        {
                            entitledBedrroms = int.Parse(data[i][61]?.ToString().Trim());
                        }
                        catch (Exception) { }

                        int? actualBedrroms = null;
                        try
                        {
                            actualBedrroms = int.Parse(data[i][62]?.ToString().Trim());
                        }
                        catch (Exception) { }

                        string typeofProperty = "";
                        try
                        {
                            typeofProperty = data[i][63]?.ToString().ToLower().Trim();
                            if (typeofProperty.Length <= 1)
                                typeofProperty = "";

                        }
                        catch (Exception) { }

                        int? rentDueDate = null;
                        try
                        {
                            rentDueDate = int.Parse(data[i][64]?.ToString().Trim());
                        }
                        catch (Exception) { }


                        DateTime? tenancyStartDate = null;
                        try
                        {
                            var dateStr = data[i][65]?.ToString().ToLower().Trim();
                            tenancyStartDate = DateTime.FromOADate(double.Parse(dateStr));
                        }
                        catch (Exception) { }

                        DateTime? tenancyEndDate = null;
                        try
                        {
                            var dateStr = data[i][66]?.ToString().ToLower().Trim();
                            tenancyEndDate = DateTime.FromOADate(double.Parse(dateStr));
                        }
                        catch (Exception) { }

                        string monthNoticePeriod = "";
                        try
                        {
                            monthNoticePeriod = data[i][68]?.ToString().ToLower().Trim();
                        }
                        catch (Exception) { }

                        decimal? initialRent = null;
                        try
                        {
                            initialRent = decimal.Parse(data[i][69].ToString());
                        }
                        catch (Exception) { }

                        decimal? currentRent = null;
                        try
                        {
                            currentRent = decimal.Parse(data[i][70].ToString());
                        }
                        catch (Exception) { }


                        DateTime? companyHiringDate = null;
                        try
                        {
                            var dateStr = data[i][20]?.ToString().ToLower().Trim();
                            companyHiringDate = DateTime.FromOADate(double.Parse(dateStr));
                        }
                        catch (Exception) { }

                        DateTime? birthDate = null;
                        try
                        {
                            var dateStr = data[i][17]?.ToString().ToLower().Trim();
                            birthDate = DateTime.FromOADate(double.Parse(dateStr));
                        }
                        catch (Exception) { }




                        //    Employee employee = await context.Employees
                        //.Include(e => e.LocalPlus)
                        //.Include(e => e.standardEmployeeCategory)
                        //.Include(e => e.location)
                        //.Include(e => e.businessUnit)
                        //.Include(e => e.organisationUnit)
                        //.Include(e => e.workingCostCentre)
                        //.Include(e => e.position)
                        //.Include(e => e.professionalArea)
                        //.Include(e => e.homeCompany)
                        //.Include(e => e.CountryofBirth)
                        //.Include(e => e.Nationality)
                        //.Include(e => e.SpouseNationality)
                        //.Include(e => e.city)
                        //.Include(e => e.typeOfVISA)
                        //.Include(e => e.assignmentStatus)
                        //.Include(e => e.Children)
                        //.Include(e => e.familyStatus)
                        //.Include(e => e.activityStatus)
                        ////.Where(e => e.activityStatus.Description.ToLower().Trim() != "leaver")
                        //.SingleOrDefaultAsync(e => e.EmployeeID == employeeNo);

                        Employee employee = employeeDic[employeeNo];

                        if (employee == null)
                        {
                            ////**** New Employee Found  *****
                            EmployeeViewModel employeeVM = new EmployeeViewModel();
                            employeeVM.employeeID = employeeNo;
                            employeeVM.name = name;
                            employeeVM.surname = surname;
                            employeeVM.technicalID = technicalID;
                            employeeVM.workingLocation = workingLocation;
                            employeeVM.employeeCategory = employeeCat;
                            employeeVM.organizationUnit = organizationUnit;

                            if (!string.IsNullOrEmpty(positionCode) && !string.IsNullOrEmpty(positionDesc))
                            {
                                employeeVM.position = positionCode + "|" + positionDesc;
                            }

                            if (!string.IsNullOrEmpty(costCenterCode) && !string.IsNullOrEmpty(costCenterDesc))
                            {
                                employeeVM.costCentre = costCenterCode + "|" + costCenterDesc;
                            }

                            employeeVM.professionalArea = professionalArea;
                            employeeVM.homeCompany = homeCompany;
                            employeeVM.gender = gender;
                            employeeVM.countryOfBirth = countryofBirth;
                            employeeVM.nationality = nationality;
                            employeeVM.familyStatus = familyStatus;
                            employeeVM.followingPartner = followingPartner;
                            employeeVM.followingChildren = followingChildren;
                            employeeVM.emailAddress = emailAddress;
                            employeeVM.activityStatus = "Active";

                            Employee newEmployee = new Employee();
                            newEmployee.EmployeeID = employeeNo;
                            newEmployee.UpdateFromEmployeeViewModel(context, employeeVM);

                            context.Employees.Add(newEmployee);

                            Housing housing = new Housing();
                            housing.HousingID = newEmployee.EmployeeID;
                            housing.employee = newEmployee;
                            housing.HomeAddressUK = homeAddress;
                            housing.EntitledUnFurnishedAllowanceWeek = unfurnishedWeek;
                            housing.DifferenceAllowanceMonthlyCostsPaid = diffAllowanceMonthlyPaid;
                            housing.FurnitureAllowance = furnitureAllowance;
                            housing.ActualFurnitureCosts = actualFurnitureCosts;
                            housing.ParkingCharges = parkingCharges;
                            housing.RegularPayrollDeduction = regularPayrollDeduction;
                            housing.UtilitiesIncluded = utilitiesIncuded;
                            housing.FurnishedUnFurnished = furnishedUnfurnished;
                            housing.HousingComments = housingComments;
                            context.Housing.Add(housing);
                        }
                        else
                        {
                            //Housing housing = context.Housing.Find(employeeNo);
                            Housing housing = housingDic[employeeNo];

                            EmployeeViewModel employeeVM = new EmployeeViewModel(employee);

                            employeeVM.activityStatus = "Active";


                            if (!string.IsNullOrEmpty(name) && employee.Name.ToLower() != name.ToLower())
                            {
                                employeeVM.name = name;

                            }
                            if (!string.IsNullOrEmpty(surname) && employee.Surname.ToLower() != surname.ToLower())
                            {
                                employeeVM.surname = surname;
                            }
                            if (!string.IsNullOrEmpty(technicalID) && !string.IsNullOrEmpty(employee.UserId) && employee.UserId.ToLower() != technicalID.ToLower())
                            {
                                employeeVM.technicalID = technicalID;
                            }
                            if (!string.IsNullOrEmpty(workingLocation) && employee.location?.Description.Trim().ToLower() != workingLocation.ToLower())
                            {
                                employeeVM.workingLocation = workingLocation;
                            }
                            if (!string.IsNullOrEmpty(employeeCat) && employee.standardEmployeeCategory?.Description.Trim().ToLower() != employeeCat.ToLower())
                            {
                                employeeVM.employeeCategory = employeeCat;
                            }
                            if (!string.IsNullOrEmpty(organizationUnit) && employee.organisationUnit?.Description.Trim().ToLower() != organizationUnit.ToLower())
                            {
                                employeeVM.organizationUnit = organizationUnit;
                            }
                            if (!string.IsNullOrEmpty(costCenterCode) && !string.IsNullOrEmpty(costCenterDesc) && employee.workingCostCentre?.ID.Trim().ToLower() != costCenterCode.ToLower())
                            {
                                employeeVM.costCentre = costCenterCode + "|" + costCenterDesc;
                            }

                            if (!string.IsNullOrEmpty(positionDesc) && !string.IsNullOrEmpty(positionCode) && employee.position?.ID.ToLower() != positionCode.ToLower() && employee.position?.Description.ToLower() != positionDesc.ToLower())
                            {
                                employeeVM.position = positionCode + "|" + positionDesc;
                            }
                            if (!string.IsNullOrEmpty(professionalArea) && employee.professionalArea?.Description.Trim().ToLower() != professionalArea.ToLower())
                            {
                                employeeVM.professionalArea = professionalArea;
                            }
                            if (!string.IsNullOrEmpty(homeCompany) && employee.homeCompany?.Description.Trim().ToLower() != homeCompany.ToLower())
                            {
                                employeeVM.homeCompany = homeCompany;
                            }
                            if (!string.IsNullOrEmpty(gender) && employee.GenderString.ToLower() != gender.ToLower())
                            {
                                employeeVM.gender = gender;
                            }
                            if (!string.IsNullOrEmpty(countryofBirth) && employee.CountryofBirth?.CountryName.Trim().ToLower() != countryofBirth.ToLower())
                            {
                                employeeVM.countryOfBirth = countryofBirth;
                            }
                            if (!string.IsNullOrEmpty(nationality) && employee.Nationality?.CountryName.Trim().ToLower() != nationality.ToLower())
                            {
                                employeeVM.nationality = nationality;
                            }
                            if (!string.IsNullOrEmpty(familyStatus) && employee.familyStatus?.Description.Trim().ToLower() != familyStatus.ToLower())
                            {
                                employeeVM.familyStatus = familyStatus;
                            }
                            if (followingPartner.HasValue && employee.FollowingPartner.HasValue && employee.FollowingPartner.Value != followingPartner.Value)
                            {
                                employeeVM.followingPartner = followingPartner;
                            }
                            else if (followingPartner.HasValue && !employee.FollowingPartner.HasValue)
                            {
                                employeeVM.followingPartner = followingPartner;
                            }

                            if (followingChildren.HasValue && employee.FollowingChildren != followingChildren.Value)
                            {
                                employeeVM.followingChildren = followingChildren;
                            }
                            if (!string.IsNullOrEmpty(emailAddress) && employee.EmailAddress?.Trim().ToLower() != emailAddress.ToLower())
                            {
                                employeeVM.emailAddress = emailAddress;
                            }

                            if (!string.IsNullOrEmpty(homeAddress) && housing.HomeAddressUK?.Trim().ToLower() != homeAddress.ToLower())
                            {
                                housing.HomeAddressUK = homeAddress;
                            }

                            if (!string.IsNullOrEmpty(housingComments) && housing.HousingComments?.Trim().ToLower() != housingComments.ToLower())
                            {
                                housing.HousingComments = housingComments;
                            }

                            if (unfurnishedWeek.HasValue && housing.EntitledUnFurnishedAllowanceWeek.HasValue && Math.Abs(housing.EntitledUnFurnishedAllowanceWeek.Value - unfurnishedWeek.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                housing.EntitledUnFurnishedAllowanceWeek = unfurnishedWeek;
                            }
                            else if (unfurnishedWeek.HasValue && !housing.EntitledUnFurnishedAllowanceWeek.HasValue)
                            {
                                housing.EntitledUnFurnishedAllowanceWeek = unfurnishedWeek;
                            }


                            if (diffAllowanceMonthlyPaid.HasValue && housing.DifferenceAllowanceMonthlyCostsPaid.HasValue && Math.Abs(housing.DifferenceAllowanceMonthlyCostsPaid.Value - diffAllowanceMonthlyPaid.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                housing.DifferenceAllowanceMonthlyCostsPaid = diffAllowanceMonthlyPaid;
                            }
                            else if (diffAllowanceMonthlyPaid.HasValue && !housing.DifferenceAllowanceMonthlyCostsPaid.HasValue)
                            {
                                housing.DifferenceAllowanceMonthlyCostsPaid = diffAllowanceMonthlyPaid;
                            }

                            if (furnitureAllowance.HasValue && housing.FurnitureAllowance.HasValue && Math.Abs(housing.FurnitureAllowance.Value - furnitureAllowance.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                housing.FurnitureAllowance = furnitureAllowance;
                            }
                            else if (furnitureAllowance.HasValue && !housing.FurnitureAllowance.HasValue)
                            {
                                housing.FurnitureAllowance = furnitureAllowance;
                            }

                            if (actualFurnitureCosts.HasValue && housing.ActualFurnitureCosts.HasValue && Math.Abs(housing.ActualFurnitureCosts.Value - actualFurnitureCosts.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                housing.ActualFurnitureCosts = actualFurnitureCosts;
                            }
                            else if (actualFurnitureCosts.HasValue && !housing.ActualFurnitureCosts.HasValue)
                            {
                                housing.ActualFurnitureCosts = actualFurnitureCosts;
                            }

                            if (parkingCharges.HasValue && housing.ParkingCharges.HasValue && Math.Abs(housing.ParkingCharges.Value - parkingCharges.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                housing.ParkingCharges = parkingCharges;
                            }
                            else if (parkingCharges.HasValue && !housing.ParkingCharges.HasValue)
                            {
                                housing.ParkingCharges = parkingCharges;
                            }

                            if (regularPayrollDeduction.HasValue && housing.RegularPayrollDeduction.HasValue && Math.Abs(housing.RegularPayrollDeduction.Value - regularPayrollDeduction.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                housing.RegularPayrollDeduction = regularPayrollDeduction;
                            }
                            else if (regularPayrollDeduction.HasValue && !housing.RegularPayrollDeduction.HasValue)
                            {
                                housing.RegularPayrollDeduction = regularPayrollDeduction;
                            }

                            if (!string.IsNullOrEmpty(utilitiesIncuded) && housing.UtilitiesIncluded?.Trim().ToLower() != utilitiesIncuded.ToLower())
                            {
                                housing.UtilitiesIncluded = utilitiesIncuded;
                            }

                            if (!string.IsNullOrEmpty(furnishedUnfurnished) && housing.FurnishedUnFurnished?.Trim().ToLower() != furnishedUnfurnished.ToLower())
                            {
                                housing.FurnishedUnFurnished = furnishedUnfurnished;
                            }



                            if (entitledBedrroms.HasValue && housing.EntitledNumberofBedrooms.HasValue && entitledBedrroms.Value != housing.EntitledNumberofBedrooms.Value)
                            {
                                housing.EntitledNumberofBedrooms = entitledBedrroms;
                            }
                            else if (entitledBedrroms.HasValue && !housing.EntitledNumberofBedrooms.HasValue)
                            {
                                housing.EntitledNumberofBedrooms = entitledBedrroms;
                            }


                            if (actualBedrroms.HasValue && housing.ActualNumberofBedrooms.HasValue && actualBedrroms.Value != housing.ActualNumberofBedrooms.Value)
                            {
                                housing.ActualNumberofBedrooms = actualBedrroms;
                            }
                            else if (actualBedrroms.HasValue && !housing.ActualNumberofBedrooms.HasValue)
                            {
                                housing.ActualNumberofBedrooms = actualBedrroms;
                            }

                            if (!string.IsNullOrEmpty(typeofProperty) && housing.TypeOfProperty?.Trim().ToLower() != typeofProperty.ToLower())
                            {
                                housing.TypeOfProperty = typeofProperty;
                            }

                            if (rentDueDate.HasValue && housing.RentDueDate.HasValue && rentDueDate.Value != housing.RentDueDate.Value)
                            {
                                housing.RentDueDate = rentDueDate;
                            }
                            else if (rentDueDate.HasValue && !housing.RentDueDate.HasValue)
                            {
                                housing.RentDueDate = rentDueDate;
                            }

                            if (tenancyStartDate.HasValue && housing.TenancyAgreementStartDate.HasValue && tenancyStartDate.Value.Date != housing.TenancyAgreementStartDate.Value.Date)
                            {
                                housing.TenancyAgreementStartDate = tenancyStartDate;
                            }
                            else if (tenancyStartDate.HasValue && !housing.TenancyAgreementStartDate.HasValue)
                            {
                                housing.TenancyAgreementStartDate = tenancyStartDate;
                            }

                            if (tenancyEndDate.HasValue && housing.TenancyAgreementEndDate.HasValue && tenancyEndDate.Value.Date != housing.TenancyAgreementEndDate.Value.Date)
                            {
                                housing.TenancyAgreementEndDate = tenancyEndDate;
                            }
                            else if (tenancyEndDate.HasValue && !housing.TenancyAgreementEndDate.HasValue)
                            {
                                housing.TenancyAgreementEndDate = tenancyEndDate;
                            }


                            if (!string.IsNullOrEmpty(monthNoticePeriod) && housing.MonthNoticePeriod?.Trim().ToLower() != monthNoticePeriod.ToLower())
                            {
                                housing.MonthNoticePeriod = monthNoticePeriod;
                            }

                            if (initialRent.HasValue && housing.InitialHouseContractRent.HasValue && Math.Abs(housing.InitialHouseContractRent.Value - initialRent.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                housing.InitialHouseContractRent = initialRent;
                            }
                            else if (initialRent.HasValue && !housing.InitialHouseContractRent.HasValue)
                            {
                                housing.InitialHouseContractRent = initialRent;
                            }

                            if (currentRent.HasValue && housing.CurrentHouseRental.HasValue && Math.Abs(housing.CurrentHouseRental.Value - currentRent.Value).CompareTo(DIFFERENCEAMOUNT) > 0)
                            {
                                housing.CurrentHouseRental = currentRent;
                            }
                            else if (currentRent.HasValue && !housing.CurrentHouseRental.HasValue)
                            {
                                housing.CurrentHouseRental = currentRent;
                            }


                            if (companyHiringDate.HasValue && employee.CompanyHiringDate.HasValue && companyHiringDate.Value.Date != employee.CompanyHiringDate.Value.Date)
                            {
                                employeeVM.companyHiringDate = companyHiringDate;
                            }
                            else if (companyHiringDate.HasValue && !employee.CompanyHiringDate.HasValue)
                            {
                                employeeVM.companyHiringDate = companyHiringDate;
                            }

                            if (birthDate.HasValue && employee.BirthDate.HasValue && birthDate.Value.Date != employee.BirthDate.Value.Date)
                            {
                                employeeVM.birthDate = birthDate;
                            }
                            else if (birthDate.HasValue && !employee.BirthDate.HasValue)
                            {
                                employeeVM.birthDate = birthDate;
                            }

                            employee.UpdateFromEmployeeViewModel(this.context,employeeVM);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                await this.context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
