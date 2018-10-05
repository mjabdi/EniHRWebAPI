using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EniHRWebAPI.ViewModel;
using EniHRWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EniHRWebAPI.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        
        private readonly MyDBContext context;

        public EmployeeController( MyDBContext _context)
        {
            this.context = _context;
        }

        [HttpGet("max")]
        public long GetMaxEmployeeNumber()
        {
            return context.Employees.Max(x => x.EmployeeID);
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<List<EmployeeViewModel>>> GetAll()
        {

            string username = AuthController.CurrentUserName(Request);
            if (String.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            List<Employee> employeeList = await context.Employees.Include(e => e.Assignment)
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
                        .Include(e => e.activityStatus).ToListAsync();

            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

            foreach (Employee employee in employeeList)
            {
                employees.Add(new EmployeeViewModel(employee));
            }

            return employees;
        }


        // GET: api/employee/active
        [HttpGet("active")]
        public async Task<ActionResult<List<EmployeeViewModel>>> GetAllActive()
        {

            string username = AuthController.CurrentUserName(Request);
            if (String.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            List<Employee> employeeList = await context.Employees.Include(e => e.Assignment)
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
                        .Where(e => e.activityStatus.Description.ToLower().Trim() != "leaver" )
                        .ToListAsync();

            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

            foreach (Employee employee in employeeList)
            {
                employees.Add(new EmployeeViewModel(employee));
            }

            return employees;
        }



        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> FindEmployee(long id)
        {
            Employee employee = await context.Employees
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
                        .Where(e => e.activityStatus.Description.ToLower().Trim() != "leaver")
                                             .SingleOrDefaultAsync(e => e.EmployeeID == id);
                         
            return new EmployeeViewModel(employee);
        }





        [HttpGet("completeness")]
        public async Task<double> GetCompleteness()
        {
            List<Employee> employeeList = await context.Employees.Include(e => e.Assignment)
            .Include(e => e.LocalPlus)
            .Include(e => e.standardEmployeeCategory)
            .Include(e => e.location)
            .Include(e => e.businessUnit)
            .Include(e => e.organisationUnit)
            .Include(e => e.workingCostCentre)
            .Include(e => e.position)
            .Include(e => e.professionalArea)
            .Include(e => e.homeCompany)
            .Include(e => e.LocalPlus)
            .Include(e => e.CountryofBirth)
            .Include(e => e.Nationality)
            .Include(e => e.SpouseNationality)
            .Include(e => e.city)
            .Include(e => e.typeOfVISA)
            .Include(e => e.assignmentStatus)
            .Include(e => e.Children)
            .Include(e => e.familyStatus)
            .Include(e => e.activityStatus)
            .Where(e => e.activityStatus.Description.ToLower().Trim() != "leaver")
            .ToListAsync();


            double totalCompleteness = 0.0;  
            foreach (Employee employee in employeeList)
            {
                totalCompleteness += new EmployeeViewModel(employee).CalcCompleteness();
            }

            return (totalCompleteness / employeeList.Count);
        }


        [HttpGet("activecount")]
        public async Task<int> GetActiveCounts()
        {
            return await context.Employees
            .Include(e => e.activityStatus)
            .Where(e => e.activityStatus.Description.ToLower().Trim() != "leaver")
                                .CountAsync();
        }



        // POST api/employee
        [HttpPost]
        public IActionResult Post([FromBody]EmployeeViewModel employee)
        {
            Employee oldEmployee = context.Employees.Find(employee.employeeID);
            if (oldEmployee != null)
                return BadRequest("DuplicateEmployeeID");

            try
            {
                Employee newEmployee = new Employee();
                newEmployee.EmployeeID = employee.employeeID;
                newEmployee.UpdateFromEmployeeViewModel(context, employee);
                context.Employees.Add(newEmployee);

                Housing housing = new Housing();
                housing.HousingID = newEmployee.EmployeeID;
                housing.employee = newEmployee;
                context.Housing.Add(housing);

                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return Ok(employee);
        }

        // PUT api/employees/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]EmployeeViewModel employee)
        {

            Employee oldEmployee = context.Employees.Find(id);
            if (oldEmployee == null)
                return NotFound();

            try
            {
                oldEmployee.UpdateFromEmployeeViewModel(context, employee);
                context.SaveChanges();
            }catch(Exception ex)
            {
                throw ex;
            }

            return Ok(employee);
        }

        // DELETE api/employees/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Employee employee = context.Employees.Find(id);
            context.Housing.Remove(context.Housing.Find(id));
            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok(employee);
        }
    }
}
