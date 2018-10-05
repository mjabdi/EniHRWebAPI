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
    [Route("api/housing")]
    public class HousingController : Controller
    {


        private readonly MyDBContext context;

        public HousingController(MyDBContext _context)
        {
            this.context = _context;
        }

        // GET: api/housing
        [HttpGet]
        public async Task<ActionResult<List<HousingViewModel>>> GetAll()
        {

            List<Housing> housingList = await context.Housing.Include(e => e.employee)
                                                     .Include(e => e.employee.LocalPlus)
                                                     .Include(e => e.employee.standardEmployeeCategory)
                                                     .Include(e => e.employee.location)
                                                     .Include(e => e.employee.workingCostCentre)
                                                     .Include(e => e.employee.familyStatus)
                                                     .Include(e => e.employee.activityStatus).OrderBy(e => e.employee.EmployeeID).ToListAsync();

            List<HousingViewModel> housings = new List<HousingViewModel>();

            foreach (Housing housing in housingList)
            {
                housings.Add(new HousingViewModel(housing));
            }

            return housings;
        }

        // GET: api/housing
        [HttpGet("active")]
        public async Task<ActionResult<List<HousingViewModel>>> GetAllActive()
        {

            List<Housing> housingList = await context.Housing.Include(e => e.employee)
                                                     .Include(e => e.employee.LocalPlus)
                                                     .Include(e => e.employee.standardEmployeeCategory)
                                                     .Include(e => e.employee.location)
                                                     .Include(e => e.employee.workingCostCentre)
                                                     .Include(e => e.employee.familyStatus)
                                                     .Include(e => e.employee.activityStatus)
                                                     .Where(e => e.employee.activityStatus.Description.ToLower().Trim() != "leaver")
                                                     .OrderBy(e => e.employee.EmployeeID).ToListAsync();

            List<HousingViewModel> housings = new List<HousingViewModel>();

            foreach (Housing housing in housingList)
            {
                housings.Add(new HousingViewModel(housing));
            }

            return housings;
        }


        // PUT api/housing/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]JToken  housing)
        {

            Housing oldHousing = context.Housing.Find(id);
            if (oldHousing == null)
                return NotFound();

            try
            {
                oldHousing.UpdateFromHousingViewModel(context, housing);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(housing);

        }
    }
}
