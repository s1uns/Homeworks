using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class Order
    {
        private List<Dish> _dishes;
        private decimal _price;

        public List<Dish> Dishes
        {
            get => _dishes;
        }
        public decimal Price
        {
            get => _price;
        }

        public Order(List<Dish> dishes, decimal price)
        {
            _dishes = new List<Dish>();
            _dishes.AddRange(dishes);
            _price = price;
        }
    }
}
