using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public struct Employee
    {
       private string _name;
       private string _position;

       public string Name
       {
           get => _name;
           set => _name = value;
       }
       public string Position
       {
           get => _position;
           set => _position = value;
       }

       public Employee(string name, string position)
        {
            _name = name;
            _position = position;
        }
    }
}
