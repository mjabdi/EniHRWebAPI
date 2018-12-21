using System;
using System.Collections.Generic;
using System.Linq;
using EniHRWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EniHRWebAPI.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(User user, MyDBContext context)
        {
            username = user.Username;
            name = user.Name;
            surname = user.Surname;
            email = user.Email;
            lastlogon = user.LastLogon;
            active = (user.ActiveStatus.Trim().ToLower() == "active");
            technicalid = user.TechnicalID;
            roles = user.RolesStr;

            try
            {
                var employee = context.Employees.FirstOrDefault(em => em.EmailAddress.ToLower().Trim() == user.Email.Trim().ToLower()
                                                                   || em.UserId.Trim().ToLower() == user.Username.Trim().ToLower()
                                                                   || (em.Name.Trim().ToLower() == user.Name.Trim().ToLower() && em.Surname.Trim().ToLower() == user.Surname.Trim().ToLower())     
                                                                    );
                if (employee != null)
                {
                    this.employeeid = employee.EmployeeID;
                }
            }
            catch (Exception) { }
        }

        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public DateTime? lastlogon { get; set; }
        public bool active { get; set; }
        public string technicalid { get; set; }
        public string roles { get; set; }
        public long employeeid { get; set; }
    }
}
