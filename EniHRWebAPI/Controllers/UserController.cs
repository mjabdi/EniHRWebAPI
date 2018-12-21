using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EniHRWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using EniHRWebAPI.ViewModel;
using Newtonsoft.Json.Linq;

namespace EniHR.Controllers
{
    [Route("api/user")]

    public class UserController : Controller
    {
        private readonly MyUsersDBContext contextUsers;
        private readonly MyDBContext context;

        public UserController( MyUsersDBContext _contextUsers, MyDBContext _context)
        {
            this.contextUsers = _contextUsers;
            this.context = _context;
        }


        // GET : api/user
        [HttpGet]
        public IEnumerable<UserViewModel> GetAll()   
        {
            var users = contextUsers.Users.ToList();
            foreach (var user in users)
            {
                yield return new UserViewModel(user,context);
            }
        }


        // GET : api/user/active
        [HttpGet("active")]
        public IEnumerable<UserViewModel> GetAllActives()
        {
            var users = contextUsers.Users.Where(usr => usr.ActiveStatus.Trim().ToLower() == "active").ToList();
            foreach (var user in users)
            {
                yield return new UserViewModel(user,context);
            }
        }


        // GET : api/user/{username}
        [HttpGet("{username}")]
        public ActionResult<UserViewModel> GetByUsername(string username)
        {
            var user = contextUsers.Users.Find(username);
            if (user == null)
            {
                return NotFound();
            }

            return new UserViewModel(user,context);
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserViewModel userVM)
        {

            if (contextUsers.Users.Find(userVM.username) != null || userVM.username.Trim().ToLower() == "admin")
                return BadRequest("DuplicateUsername");

            var user = new User();
            user.UpdateFromUserViewModel(userVM);
            user.IsFirstLogon = true;
            user.Password = "123456";

            contextUsers.Users.Add(user);
            await contextUsers.SaveChangesAsync();
            return Ok();
        }

        // PUT: User/{username}
        [HttpPut("{username}")]
        public async Task<IActionResult> Update(string username , [FromBody] UserViewModel userVM)
        {
            var _user = contextUsers.Users.Find(username);
            if (_user == null)
            {
                return NotFound();
            }

            _user.UpdateFromUserViewModel(userVM);

            await contextUsers.SaveChangesAsync();
            return Ok();
        }

        // DELETE: User/{username}
        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var _user = contextUsers.Users.Find(username);
            if (_user == null)
            {
                return NotFound();
            }

            contextUsers.Users.Remove(_user);
            await contextUsers.SaveChangesAsync();
            return Ok();
        }
    }
}