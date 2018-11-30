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
    [Route("api/ict")]
    public class ICTController : Controller
    {
        private readonly MyDBContext context;

        public ICTController(MyDBContext _context)
        {
            this.context = _context;
        }


        // GET: api/ict
        [HttpGet]
        public async Task<ActionResult<List<ICTViewModel>>> GetAll()
        {

            List<ICT> ictList = await context.ICTs.Include(e => e.employee)
                                                     .Include(e => e.employee.LocalPlus)
                                                     .Include(e => e.employee.standardEmployeeCategory)
                                                     .Include(e => e.employee.location)
                                                     .Include(e => e.employee.workingCostCentre)
                                                     .Include(e => e.employee.familyStatus)
                                                     .Include(e => e.employee.activityStatus).OrderBy(e => e.employee.EmployeeID).ToListAsync();

            List<ICTViewModel> icts = new List<ICTViewModel>();

            foreach (ICT ict in ictList)
            {
                icts.Add(new ICTViewModel(ict));
            }

            return icts;
        }


        // PUT api/ict/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]JToken ict)
        {

            ICT oldICT = context.ICTs.Find(id);
            if (oldICT == null)
                return NotFound();

            try
            {
                oldICT.UpdateFromICTViewModel(context, ict);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(ict);
        }
    }
}
