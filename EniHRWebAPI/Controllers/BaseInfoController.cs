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
            return await context.LocalPlus.Select(x => x.Description.Trim()).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("localplustable")]
        public async Task<IEnumerable<object>> GetLocalPlusTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.LocalPlus.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }       
        }

        [HttpPut("localplustable")]
        public async Task<IActionResult> UpdateLocalPlusTableAsync([FromBody] Item item)
        {
            try
            {
                List<LocalPlus> records = await context.LocalPlus.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("workinglocation")]
        public async Task<IEnumerable<string>> GetWorkingLocationAsync()
        {
            return await context.Location.Select(x => x.Description.Trim()).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("workinglocationtable")]
        public async Task<IEnumerable<object>> GetWorkingLocationTableAsync()
        {
            try
            {
                IEnumerable<Item> items =  await context.Location.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim()}).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("workinglocationtable")]
        public async Task<IActionResult> UpdateWorkingLocationTableAsync([FromBody] Item item)
        {
            try
            {
                List<Location> records = await context.Location.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach(var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("employeecategory")]
        public async Task<IEnumerable<string>> GetEmployeeCategoryAsync()
        {
            return await context.StandardEmployeeCategory.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("employeecategorytable")]
        public async Task<IEnumerable<object>> GetEmployeeCategoryTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.StandardEmployeeCategory.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("employeecategorytable")]
        public async Task<IActionResult> UpdateStandardEmployeeCategoryTableAsync([FromBody] Item item)
        {
            try
            {
                List<StandardEmployeeCategory> records = await context.StandardEmployeeCategory.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("organizationunit")]
        public async Task<IEnumerable<string>> GetOrganiationUnitAsync()
        {
            return await context.OrganisationUnit.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }


        [HttpGet("organizationunittable")]
        public async Task<IEnumerable<object>> GetOrganiationUnitTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.OrganisationUnit.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("organizationunittable")]
        public async Task<IActionResult> UpdateOrganiationUnitTableAsync([FromBody] Item item)
        {
            try
            {
                List<OrganisationUnit> records = await context.OrganisationUnit.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("costcentre")]
        public async Task<IEnumerable<string>> GetCostCentreAsync()
        {
            return await context.WorkingCostCentre.Select(x => (x.ID + " | " + x.Description )).OrderBy(x => x).Where(x => x != null && x.Trim() != "" && x.Trim() != "|" ).Distinct().ToListAsync();
        }

        [HttpGet("costcentretable")]
        public async Task<IEnumerable<object>> GetCostCentreTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.WorkingCostCentre.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("costcentretable")]
        public async Task<IActionResult> UpdateCostCentreTableAsync([FromBody] Item item)
        {
            try
            {
                List<WorkingCostCentre> records = await context.WorkingCostCentre.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("position")]
        public async Task<IEnumerable<string>> GetPositionAsync()
        {
            return await context.Position.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("positiontable")]
        public async Task<IEnumerable<object>> GetPositionTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.Position.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("positiontable")]
        public async Task<IActionResult> UpdatePositionTableAsync([FromBody] Item item)
        {
            try
            {
                List<Position> records = await context.Position.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("professionalarea")]
        public async Task<IEnumerable<string>> GetProfessionalAreaAsync()
        {
            return await context.ProfessionalArea.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("professionalareatable")]
        public async Task<IEnumerable<object>> GetProfessionalAreaTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.ProfessionalArea.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("professionalareatable")]
        public async Task<IActionResult> UpdateProfessionalAreaTableAsync([FromBody] Item item)
        {
            try
            {
                List<ProfessionalArea> records = await context.ProfessionalArea.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("homecompany")]
        public async Task<IEnumerable<string>> GetHomeCompanyAsync()
        {
            return await context.HomeCompany.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("homecompanytable")]
        public async Task<IEnumerable<object>> GetHomeCompanyTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.HomeCompany.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("homecompanytable")]
        public async Task<IActionResult> UpdateHomeCompanyTableAsync([FromBody] Item item)
        {
            try
            {
                List<HomeCompany> records = await context.HomeCompany.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("country")]
        public async Task<IEnumerable<string>> GetCountryAsync()
        {
            return await context.Country.Select(x => x.CountryName).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("countrytable")]
        public async Task<IEnumerable<object>> GetCountryTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.Country.Select(x => new Item { ID = x.ID.ToString(), Description = x.CountryName.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("countrytable")]
        public async Task<IActionResult> UpdateCountryTableAsync([FromBody] Item item)
        {
            try
            {
                List<Country> records = await context.Country.Where(x => x.CountryName.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.CountryName = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("familystatus")]
        public async Task<IEnumerable<string>> GetFamilyStatusAsync()
        {
            return await context.FamilyStatus.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("familystatustable")]
        public async Task<IEnumerable<object>> GetFamilyStatusTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.HomeCompany.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("familystatustable")]
        public async Task<IActionResult> UpdateFamilyStatusTableAsync([FromBody] Item item)
        {
            try
            {
                List<FamilyStatus> records = await context.FamilyStatus.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("typeofvisa")]
        public async Task<IEnumerable<string>> GetTypeofVisaAsync()
        {
            return await context.TypeOfVISA.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("typeofvisatable")]
        public async Task<IEnumerable<object>> GetTypeofVisaTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.TypeOfVISA.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("typeofvisatable")]
        public async Task<IActionResult> UpdateTypeofVisaTableAsync([FromBody] Item item)
        {
            try
            {
                List<TypeOfVISA> records = await context.TypeOfVISA.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("activitystatus")]
        public async Task<IEnumerable<string>> GetActivityStatusAsync()
        {
            return await context.ActivityStatus.Select(x => x.Description).OrderBy(x => x).Where(x => x != null && x.Trim() != "").Distinct().ToListAsync();
        }

        [HttpGet("activitystatustable")]
        public async Task<IEnumerable<object>> GetActivityStatusTableAsync()
        {
            try
            {
                IEnumerable<Item> items = await context.ActivityStatus.Select(x => new Item { ID = x.ID.ToString(), Description = x.Description.Trim() }).OrderBy(x => x.Description).Where(x => x.Description != null && x.Description.Trim() != "").Distinct().ToListAsync();
                List<Item> filtered = new List<Item>();
                foreach (Item item in items)
                {
                    if (!filtered.Contains(item))
                    {
                        filtered.Add(item);
                    }
                }
                return filtered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("activitystatustable")]
        public async Task<IActionResult> UpdateActivityStatusTableAsync([FromBody] Item item)
        {
            try
            {
                List<ActivityStatus> records = await context.ActivityStatus.Where(x => x.Description.Trim() == item.OldDescription.Trim()).ToListAsync();
                foreach (var obj in records)
                {
                    obj.Description = item.Description.Trim();
                }
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Item : IEquatable<Item>, IEqualityComparer<Item>
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string OldDescription { get; set; }

        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }

        public bool Equals(Item other)
        {
            return this.Description.Trim().ToLower() == other.Description.Trim().ToLower();
        }

        public bool Equals(Item x, Item y)
        {
            return x.Description.Trim().ToLower() == y.Description.Trim().ToLower();
        }

        public int GetHashCode(Item obj)
        {
            return obj.Description.GetHashCode();
        }


    }

    public class ItemComparer : Item, IEquatable<Item>, IEqualityComparer<Item>
    {

        public ItemComparer(string id, string desc)
        {
            ID = id;
            Description = desc;
        }

        public ItemComparer()
        {
            
        }

        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }

        public bool Equals(Item other)
        {
            return this.Description.Trim().ToLower() == other.Description.Trim().ToLower();
        }

        public bool Equals(Item x, Item y)
        {
            return x.Description.Trim().ToLower() == y.Description.Trim().ToLower();
        }

        public int GetHashCode(Item obj)
        {
            return obj.Description.GetHashCode();
        }
    }
}
