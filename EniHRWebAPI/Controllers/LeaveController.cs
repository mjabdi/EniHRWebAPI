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
    [Route("api/leave")]
    public class LeaveController : Controller
    {

        private readonly MyDBContext context;

        public LeaveController(MyDBContext _context)
        {
            this.context = _context;
        }


        // GET: api/leave
        [HttpGet]
        public async Task<ActionResult<List<LeaveViewModel>>> GetAll()
        {

            List<Leave> leaveList = await context.Leaves.Include(e => e.employee)
                                                     .Include(e => e.employee.LocalPlus)
                                                     .Include(e => e.employee.standardEmployeeCategory)
                                                     .Include(e => e.employee.location)
                                                     .Include(e => e.employee.workingCostCentre)
                                                     .Include(e => e.employee.familyStatus)
                                                     .Include(e => e.employee.activityStatus)
                                                 .OrderByDescending(e => e.RegisteredDataTime)
                                                 .ToListAsync();

            List<LeaveViewModel> leaveVMList = new List<LeaveViewModel>();

            foreach (Leave leave in leaveList)
            {
                leaveVMList.Add(new LeaveViewModel(leave));
            }

            return leaveVMList;
        }



        // Post: api/search
        [HttpPost("search")]
        public async Task<ActionResult<List<LeaveViewModel>>> SearchLeave([FromBody] LeaveSearchViewModel criteria)
        {

            List<Leave> leaveList = await context.Leaves.Include(e => e.employee)
                                                     .Include(e => e.employee.LocalPlus)
                                                     .Include(e => e.employee.standardEmployeeCategory)
                                                     .Include(e => e.employee.location)
                                                     .Include(e => e.employee.workingCostCentre)
                                                     .Include(e => e.employee.familyStatus)
                                                     .Include(e => e.employee.activityStatus)
                                                 .OrderByDescending(e => e.RegisteredDataTime)
                                                 .ToListAsync();

            List<LeaveViewModel> leaveVMList = new List<LeaveViewModel>();

            foreach (Leave leave in leaveList)
            {
                LeaveViewModel leaveVM = new LeaveViewModel(leave);
                if (criteria.MeetLeave(leaveVM))
                {
                    leaveVMList.Add(leaveVM);
                }
            }

            return leaveVMList;
        }




        // POST api/leave
        [HttpPost]
        public IActionResult Post([FromBody]LeaveViewModel leaveVM)
        {
            try
            {
                Leave leave = leaveVM.GetLeave(context);
                context.Leaves.Add(leave);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(leaveVM);
        }

        // PUT api/leave/{id}
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]LeaveViewModel leaveVM)
        {

            Leave oldLeave = context.Leaves.Find(id);
            if (oldLeave == null)
                return NotFound();

            try
            {
                oldLeave.updateLeaveFromViewModel(context, leaveVM);         
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(leaveVM);
        }


        // GET: api/leave/types
        [HttpGet("types")]
        public  ActionResult<IEnumerable<string>> GetLeaveTypes()
        {
            return new string[] { 
                "Annual Leave", 
                "Sick Leave",
                "Maternity Leave",
                "Paternity Leave",
                "Marriage Leave",
                "Special Leave",
                "Compassionate Leave",
                "Unpaid Leave",
                "Contract Travel"
            };
        }

        // GET: api/leave/types
        //[HttpGet("holidays")]
        //public ActionResult<IEnumerable<DateTime>> GetHolidays()
        //{
                
        //}



    }
}
