using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EniHRWebAPI.Models
{
    public class User
    {

        public User()
        {
            ActiveStatus = "Active";
            IsFirstLogon = true;
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

    }
}
