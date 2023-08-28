using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class RestaurantAdmin
    {
        public List <Employee> employees = new List<Employee>();
        public List <Table> tables = new List<Table>();
        public List <Client> clients = new List<Client>();
        public List <Dish> dishes = new List<Dish>();
        public List <Ingredient> ingredients = new List<Ingredient>();
        public List <Order> orders = new List<Order>();

        public void AddItem(List <Employee> list)
        {
            Console.WriteLine("Вкажіть ім'я працівника: ");
            string name = Console.ReadLine();
            Console.WriteLine("Вкажіть посаду працівника: ");
            string position = Console.ReadLine();
            list.Add(new Employee(name, position));
        }
        public void AddItem(List<Table> list)
        {
            Console.WriteLine("Вкажіть номер столу: ");
            int index = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Вкажіть кількість місць: ");
            int numOfSeats = Convert.ToInt32(Console.ReadLine());
            list.Add(new Table(index, numOfSeats));
        }
        public void AddItem(List<Client> list)
        {
            Console.WriteLine("Вкажіть ім'я клієнта: ");
            string name = Console.ReadLine();
            Console.WriteLine("Вкажіть номер телефону (без +380): ");
            string phoneNum = Console.ReadLine();
            list.Add(new Client(name, "+380" + phoneNum));
        }
        public void AddItem(List<Ingredient> list)
        {
            Console.WriteLine("Вкажіть назву інгредієнту: ");
            string name = Console.ReadLine();
            Console.WriteLine("Вкажіть ціну за 1 шт.: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            list.Add(new Ingredient(name, price));
        }
        public void AddItem(List<Ingredient> list1, List<Dish> list2)
        {
            Console.WriteLine("Придумайте назву для свого блюда: ");
            string name = Console.ReadLine();
            var recipe = new List <Ingredient> ();
            decimal price = 0;
            bool add = true;
            while (add)
            {
                Console.WriteLine("Додайте інгредієнт(за номером): ");
                for (int i = 0; i < list1.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {list1[i].Name}");
                }
                int chosen = Convert.ToInt32(Console.ReadLine()) - 1;
                var item = list1[chosen];
                recipe.Add(item);
                price += list1[chosen].Price;
                Console.WriteLine("Це все?\n1 - так; \n2-ні, додати ще");
                switch(Console.ReadLine())
                {
                    case "1": add = false; break;
                    case "2": continue;
                    default: Console.WriteLine("Невідома команда, спробуйте ще раз"); break;
                }
            }
            list2.Add(new Dish(name, recipe, price));
        }
        public void AddItem(List<Dish> list1, List<Order> list2)
        {
            var orderlist = new List<Dish>();
            decimal price = 0;
            bool add = true;
            while (add)
            {
                Console.WriteLine("Додайте блюдо(за номером): ");
                for (int i = 0; i < list1.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {list1[i].Name}");
                }
                int chosen = Convert.ToInt32(Console.ReadLine()) - 1;
                var item = list1[chosen];
                orderlist.Add(item);
                price += list1[chosen].Price;
                Console.WriteLine("Це все?\n1 - так; \n2-ні, додати ще");
                switch (Console.ReadLine())
                {
                    case "1": add = false; break;
                    case "2": continue;
                    default: Console.WriteLine("Невідома команда, спробуйте ще раз"); break;
                }
            }
            list2.Add(new Order(orderlist, price));
        }

        public void PrintList(List<Employee> list)
        {
            foreach (Employee item in list)
            {
                Console.WriteLine(item.Name + ", " + item.Position);
            }
        }
        public void PrintList(List<Client> list)
        {
            foreach (Client item in list)
            {
                Console.WriteLine(item.Name + ", " + item.PhoneNum);
            }
        }
        public void PrintList(List<Table> list)
        {
            foreach (Table item in list)
            {
                Console.WriteLine(item.Index + ", " + item.NumOfSeats + " місць");
            }
        }
        public void PrintList(List<Ingredient> list)
        {
            foreach (Ingredient item in list)
            {
                Console.WriteLine(item.Name + ", " + item.Price + " $");
            }
        }
        public void PrintList(List<Dish> list)
        {
            foreach (Dish item in list)
            {
                Console.WriteLine(item.Name + ", " + item.Price + " $");
            }
        }
        public void PrintList(List<Order> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                string dishes = "";
                for (int j = 0; j < list[i].Dishes.Count; j++)
                {
                    if(j < list[i].Dishes.Count - 1)
                    {
                        dishes += list[i].Dishes[j].Name + ", ";
                    }
                    else
                    {
                        dishes += list[i].Dishes[j].Name;

                    }
                }
                Console.WriteLine($"{i+1} * [{dishes}], {list[i].Price} $");
            }
        }
        public RestaurantAdmin() { }
    }
}
