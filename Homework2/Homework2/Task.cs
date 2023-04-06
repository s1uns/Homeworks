using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    internal class Task
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
        public List<User> Users { get; }


        public Task(string name, string status, List<User> users) 
        {
            Name = name;
            Status = status;
            Users = users ?? new List<User>();
        }
    }
}
