using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EniHRWebAPI.Controllers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }
    }
}
