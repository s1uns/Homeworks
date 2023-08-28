using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public struct Client
    {
        private string _name;
        private string _phoneNum;

        public string Name { 
            get => _name; 
            set => _name = value;
        }
        public string PhoneNum { 
        get => _phoneNum;   
        set => _phoneNum = value; 
        }

        public Client(string? name, string? phoneNum)
        {
            _name = name;
            _phoneNum = phoneNum;
        }
    }
}
