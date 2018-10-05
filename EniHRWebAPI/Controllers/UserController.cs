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

namespace EniHR.Controllers
{
    [Route("api/user")]

    public class UserController : Controller
    {
        private readonly MyUsersDBContext contextUsers;
        public UserController( MyUsersDBContext _contextUsers)
        {
            this.contextUsers = _contextUsers;
        }


        // GET : api/user
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()   
        {
            return await contextUsers.Users.ToListAsync();
        }

        // GET : api/user/{username}
        [HttpGet("{username}", Name = "GetUser")]
        public ActionResult<User> GetByUsername(string username)
        {
            var user = contextUsers.Users.Find(username);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            user.IsFirstLogon = true;
            user.Password = "123456";

            contextUsers.Users.Add(user);
            await contextUsers.SaveChangesAsync();
            return CreatedAtRoute("GetUser", new { username = user.Username }, user);

        }

        // PUT: User/{username}
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string username , [FromBody] User user)
        {
            var _user = contextUsers.Users.Find(username);
            if (_user == null)
            {
                return NotFound();
            }

            _user.Name = user.Name;
            _user.Surname = user.Surname;
            _user.Email = user.Email;
            _user.Password = user.Password;
            _user.ActiveStatus = user.ActiveStatus;
            _user.IsFirstLogon = user.IsFirstLogon;
            _user.LastLogon = user.LastLogon;

            await contextUsers.SaveChangesAsync();
            return NoContent();
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
            return NoContent();
        }

    }
}