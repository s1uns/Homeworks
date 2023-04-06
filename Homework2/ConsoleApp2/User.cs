using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    internal class User
    {
        public string? Name { get; set; }
        public string? Position { get; set; }

        public User(string name, string position)
        {
            Name = name;
            Position = position;
        }
    }
}
