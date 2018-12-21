using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using EniHRWebAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EniHRWebAPI.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {

        private readonly MyUsersDBContext contextUsers;
        IConfiguration configuration;

        public AuthController(MyUsersDBContext _contextUsers, IConfiguration config)
        {
            this.contextUsers = _contextUsers;
            this.configuration = config;
        }

        // POST: /api/auth/login
        [HttpPost]
        [Route("login")]    
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            // System.Threading.Thread.Sleep(5000);

            var _user = contextUsers.Users.Find(user.UserName);

            if (user.UserName == "admin")
            {
                if (user.Password == "admin$123")
                {
                    _user = new User { Username = "admin", Password = "admin$123" };
                }
                else
                {
                    return Unauthorized();
                }
            }


            if (_user == null)
            {
                return NotFound();
            }

            if (user.Password == _user.Password)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));

                var tokeOptions = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(10),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);


                _user.LastLogon = DateTime.Now;

                contextUsers.SaveChanges();

                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }



        // POST: /api/auth/changepassword
        [HttpPost]
        [Route("changepassword")]
        public IActionResult ChangePassword([FromBody]ChangePasswordDTO info)
        {
            if (info == null)
                return NotFound();


            User user = contextUsers.Users.Find(info.username);

            if (user == null)
                return NotFound();

            if (user.Password != info.oldPassword)
                return BadRequest("WrongPassword");

            user.Password = info.newPassword;

            contextUsers.SaveChanges();

            return Ok();
        }



        public static string CurrentUserName(HttpRequest request)
        {
            var tokens = request.Headers.Values;
            var accessToken = "";
            var username = "";
            foreach (var tt in tokens)
            {
                if (tt.ToString().StartsWith("Bearer:"))
                {
                    accessToken = tt.ToString().Substring(7);
                }
            }

            if (!String.IsNullOrEmpty(accessToken))
            {
                JwtSecurityToken token = new JwtSecurityToken(accessToken);
                username = token.Subject;
            }

            return username;
        }
    }
}
