using System;

namespace Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var administrator = new RestaurantAdmin();
            Console.WriteLine("Вітаємо в панелі адміністратора нашого ресторану!");
            Thread.Sleep(1500);
            Console.WriteLine("Оберіть функцію: ");
            bool keepworking = true;
            while(keepworking) { 
            Console.WriteLine("1 - додати сутність до ресторану; " +
                              "\n2 - переглянути списки сутностей ресторану; " +
                              "\n3 - вийти з програми; ");
            switch(Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Оберіть сутність:");
                    Console.WriteLine("1 - працівник; \n" +
                                      "2 - клієнт; \n" +
                                      "3 - стіл; \n" +
                                      "4 - інгредієнт; \n" +
                                      "5 - страва; \n" +
                                      "6 - замовлення;\n" +
                                      "7 - відміна");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            administrator.AddItem(administrator.employees); break;
                        case "2":
                            administrator.AddItem(administrator.clients); break;
                        case "3":
                            administrator.AddItem(administrator.tables); break;
                        case "4":
                            administrator.AddItem(administrator.ingredients); break;
                        case "5":
                            administrator.AddItem(administrator.ingredients, administrator.dishes); break;
                        case "6":
                            administrator.AddItem(administrator.dishes, administrator.orders); break;
                        case "7": 
                            break;
                        default: 
                            Console.WriteLine("Невідома команда, спробуйте ще раз."); break;
                    }
                    break;
                case "2":
                    Console.WriteLine("Оберіть сутність:");
                    Console.WriteLine("1 - працівник; \n" +
                                      "2 - клієнт; \n" +
                                      "3 - стіл; \n" +
                                      "4 - інгредієнт; \n" +
                                      "5 - страва; \n" +
                                      "6 - замовлення;\n" +
                                      "7 - відміна");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            administrator.PrintList(administrator.employees); break;
                        case "2":
                            administrator.PrintList(administrator.clients); break;
                        case "3":
                            administrator.PrintList(administrator.tables); break;
                        case "4":
                            administrator.PrintList(administrator.ingredients); break;
                        case "5":
                            administrator.PrintList(administrator.dishes); break;
                        case "6":
                            administrator.PrintList(administrator.orders); break;
                        case "7":
                            break;
                        default:
                            Console.WriteLine("Невідома команда, спробуйте ще раз."); break;
                    }
                    break;
                case "3": keepworking = false; break;
                default:
                    Console.WriteLine("Невідома команда, спробуйте ще раз."); break;
                }
            }
        }
    }
}