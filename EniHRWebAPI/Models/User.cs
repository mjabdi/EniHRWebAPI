using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EniHRWebAPI.ViewModel;

namespace EniHRWebAPI.Models
{
    public class User
    {

        public User()
        {
            ActiveStatus = "Active";
            IsFirstLogon = true;
        }

        public void UpdateFromUserViewModel(UserViewModel userVM)
        {
            Username = userVM.username;
            Name = userVM.name;
            Surname = userVM.surname;
            Email = userVM.email;
            ActiveStatus = userVM.active ? "Active" : "Disabled";
            TechnicalID = userVM.technicalid;
            RolesStr = userVM.roles;
        }


        [Key]
        public string Username { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public bool IsFirstLogon { get; set; }

        public DateTime? LastLogon { get; set; }

        public string ActiveStatus { get; set; }

        public string TechnicalID { get; set; }
        public string RolesStr { get; set; }

    }
}
