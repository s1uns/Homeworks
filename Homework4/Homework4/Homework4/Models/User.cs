using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4.Models
{
    public class User
    {
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Password { get; set; }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
