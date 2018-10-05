using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EniHRWebAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EniHRWebAPI.Controllers
{

  

    [Route("api/baseinfo")]
    public class BaseInfoController : Controller
    {
        private readonly MyDBContext context;

        public BaseInfoController(MyDBContext _context)
        {
            this.context = _context;
        }

        // GET: api/baseinfo/localplus
        [HttpGet("localplus")]
        public async Task<IEnumerable<string>> GetLocalPlusAsync()
        {
            return await context.LocalPlus.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("workinglocation")]
        public async Task<IEnumerable<string>> GetWorkingLocationAsync()
        {
            return await context.Location.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("employeecategory")]
        public async Task<IEnumerable<string>> GetEmployeeCategoryAsync()
        {
            return await context.StandardEmployeeCategory.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("organizationunit")]
        public async Task<IEnumerable<string>> GetOrganiationUnitAsync()
        {
            return await context.OrganisationUnit.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("costcentre")]
        public async Task<IEnumerable<string>> GetCostCentreAsync()
        {
            return await context.WorkingCostCentre.Select(x => (x.ID + " | " + x.Description )).OrderBy(x => x).Where(x => x != null && x.Trim() != "" && x.Trim() != "|" ).Distinct().ToListAsync();
        }

        [HttpGet("position")]
        public async Task<IEnumerable<string>> GetPositionAsync()
        {
            return await context.Position.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("professionalarea")]
        public async Task<IEnumerable<string>> GetProfessionalAreaAsync()
        {
            return await context.ProfessionalArea.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("homecompany")]
        public async Task<IEnumerable<string>> GetHomeCompanyAsync()
        {
            return await context.HomeCompany.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("country")]
        public async Task<IEnumerable<string>> GetCountryAsync()
        {
            return await context.Country.Select(x => x.CountryName).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("familystatus")]
        public async Task<IEnumerable<string>> GetFamilyStatusAsync()
        {
            return await context.FamilyStatus.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("typeofvisa")]
        public async Task<IEnumerable<string>> GetTypeofVisaAsync()
        {
            return await context.TypeOfVISA.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("activitystatus")]
        public async Task<IEnumerable<string>> GetActivityStatusAsync()
        {
            return await context.ActivityStatus.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }



    }
}
