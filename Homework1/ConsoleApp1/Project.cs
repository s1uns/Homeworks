using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    internal class Project
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Task> Tasks { get; }
        public DateTime Term { get; set; }
        public string Budget { get; set; }
        public List<User> Users { get; }

        public Project(string name, string? description, List<Task> tasks, DateTime term, string budget, List<User> users)
        {
            Name = name;
            Description = description;
            Tasks = tasks ?? new List<Task>();
            Term = term;
            Budget = budget;
            Users = users ?? new List<User>();
        }
    }
}
