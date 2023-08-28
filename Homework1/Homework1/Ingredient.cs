using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class Ingredient
    {
        private string _name;
        private decimal _price;

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public decimal Price
        {
            get => _price;
            set => _price = value;
        }

        public Ingredient(string name, decimal price)
        {
            _name = name;
            _price = price;
        }
    }
}
