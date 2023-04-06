using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class Dish
    {
        private string _name;
        private List<Ingredient> _ingredientsList;
        private decimal _price;

        public string Name
        {
            get => _name; 
            set => _name = value;
        } 
        public List<Ingredient> IngredientsList
        {
            get => _ingredientsList;
        }
        public decimal Price
        {
            get => _price;
        }

        public Dish(string name, List<Ingredient> ingredientsList, decimal price)
        {
            _name = name;
            _ingredientsList = new List<Ingredient>();
            _ingredientsList.AddRange(ingredientsList);
            _price = price;
        }
    }
}
