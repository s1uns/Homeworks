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

        public Task(string name, string description, string status) 
        {
            Name = name;
            Status = status;
        }
    }
}
