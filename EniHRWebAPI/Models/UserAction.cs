using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EniHRWebAPI.Models
{
    public class UserAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set;}

        public string Username { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string ActionType { get; set; }
        public string ActionDescription { get; set; }

    }
}
