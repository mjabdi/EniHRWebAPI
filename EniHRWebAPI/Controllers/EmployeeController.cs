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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EniHRWebAPI.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        
        private readonly MyDBContext context;

        private readonly IHostingEnvironment _hostingEnvironment;

        public EmployeeController( MyDBContext _context, IHostingEnvironment hostingEnvironment)
        {
            this.context = _context;
            _hostingEnvironment = hostingEnvironment;
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


        // POST api/employee/uploadphoto
        [HttpPost("uploadphoto/{employeeId}")]
        public async Task<IActionResult> UploadPhotoAsync(long employeeId)
        {
            try
            {
                var result = await Request.ReadFormAsync();
                string filePath = _hostingEnvironment.WebRootPath + "/profilepictures/" + employeeId + "_tmp" + ".jpg";
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(System.IO.File.Create(filePath)))
                {
                    await result.Files[0].CopyToAsync(file.BaseStream);
                }

                return Ok();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        // POST api/employee/uploadphoto
        [HttpPost("uploadsave/{employeeId}")]
        public async Task<IActionResult> UploadSaveAsync(long employeeId)
        {
            try
            {
                string tmpfilePath = _hostingEnvironment.WebRootPath + "/profilepictures/" + employeeId + "_tmp" + ".jpg";
                string filePath = _hostingEnvironment.WebRootPath + "/profilepictures/" + employeeId  + ".jpg";


                using (
                    System.IO.StreamReader reader = new System.IO.StreamReader(tmpfilePath))
                {
                    using (System.IO.StreamWriter file =
                         new System.IO.StreamWriter(System.IO.File.Create(filePath)))
                    {
                        await reader.BaseStream.CopyToAsync(file.BaseStream);
                    }

                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("images/{employeeID}/{random}")]
        public async Task<ActionResult> GetImageAsync(long employeeID,string random)
        {
            try
            {
                string _rootPath = _hostingEnvironment.WebRootPath + "/profilepictures/";
                string imagePath = employeeID + ".jpg";

                var serverPath = Path.Combine(_rootPath, imagePath);

                Byte[] b = await System.IO.File.ReadAllBytesAsync(serverPath);   // You can use your own method over here.         
                return File(b, "image/jpeg");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("images/importall")]
        public async Task<ActionResult> ImportAllAsync()
        {
            try
            {
                string _rootPath = _hostingEnvironment.WebRootPath + "/profilepictures/";
                string _serverPath = "http://my.eeep.intranet:8099/PhotoIDs/";
                string tmpfilePath = _rootPath + "user.jpg";

                List<Employee> employees = await context.Employees.ToListAsync();

                foreach (var employee in employees)
                {
                    string serverUrl = _serverPath + employee.UserId + ".jpg";
                    string filePath = _rootPath + employee.EmployeeID + ".jpg";
                    using (WebClient webClient = new WebClient())
                    {
                        try
                        {
                            byte[] data = webClient.DownloadData(serverUrl);
                            using (System.IO.BinaryWriter file =
                                 new System.IO.BinaryWriter(System.IO.File.Create(filePath)))
                            {
                                await file.BaseStream.WriteAsync(data, 0, data.Length);
                            }
                        }

                        catch (Exception)
                        {
                            using (
                                System.IO.StreamReader reader = new System.IO.StreamReader(tmpfilePath))
                            {
                                using (System.IO.StreamWriter file =
                                     new System.IO.StreamWriter(System.IO.File.Create(filePath)))
                                {
                                    await reader.BaseStream.CopyToAsync(file.BaseStream);
                                }
                            }
                        }
                    }
                }

                    return Ok();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
