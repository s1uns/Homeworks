namespace Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Вітаємо в панелі управління проєктами!");
            var program = new ProjectsAdministration(null, null);
            var work = true;
            while (work)
            {
                Console.WriteLine("Оберіть функцію: \n1 - перегнялути список проєктів \n2 - переглянути список вільних користувачів \n3 - створити проєкт \n4 - додати користувача до бази \n5 - редагувати існуючі проєкти \n6 - редагувати список вільних користувачів \n7 - вихід з програми");
                switch(Console.ReadLine())
                {
                    case "1":
                        program.PrintList(program.Projects); break;
                    case "2":
                        program.PrintList(program.Users); break;
                    case "3":
                        program.AddProject(program.Projects); break;
                    case "4":
                        program.AddUser(program.Users); break;
                    case "5":
                        program.EditProject(program.Projects); break;
                    case "6":
                        program.EditUser(program.Users); break;
                    case "7":
                        work = false; break;
                    default:
                        Console.WriteLine("Невідома команда, спробуйте ще раз."); break;
                }
            }
        }
    }
}