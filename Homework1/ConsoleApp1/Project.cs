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
        public long Budget { get; set; }
        public List<User> Users { get; }
        
    }
}
