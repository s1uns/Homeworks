using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    internal class ProjectsAdministration
    {
        public List<Project> Projects { get; }

        public void AddProject(List<Project> projects)
        {
            Console.Write("Вкажіть назву проєкту: ");
            var name = Console.ReadLine();
            Console.Write("Напишіть невеликий опис до проєкту: ");
            var description = Console.ReadLine();
            Console.Write("Виділіть кількість днів на виконання проєкту: ");
            var dateOfTerm = CreateDateOfTerm(Convert.ToInt32(Console.ReadLine()));
            Console.Write("Вкажіть бюджет, виділенний на реалізацію проєкту: ");
            var budget = Console.ReadLine() + " $";
            Projects.Add(new Project(name, description, null, dateOfTerm, budget, null));
            Console.WriteLine("Проєкт успішно створено!");

        }
        public void AddTask(Project project, List<Task> tasks)
        {
            Console.Write("Вкажіть назву завдання: ");
            var name = Console.ReadLine();
            project.Tasks.Add(new Task(name, "undone"));
            Console.WriteLine("Завдання успішно додано до проєкту!");
        }
        public void AddUser(Project project, List<User> users)
        {
            Console.Write("Вкажіть ім'я користувача: ");
            var name = Console.ReadLine();
            Console.Write("Вкажіть посаду користувача: ");
            var position = Console.ReadLine();
            project.Users.Add(new User(name, position));
            Console.WriteLine("Користувача успішно додано до проєкту!");
        }
        public DateTime StringToDate(string date)
        {
            return DateTime.ParseExact(date, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
        public DateTime CreateDateOfTerm(int term) //For creating a term of finishing the chosen project
        {
            return DateTime.Today.AddDays(term);
        }
        public void EditProject(Project project)
        {
            Console.WriteLine($"Що саме Ви бажаєте змінити: \n1 - ім'я проєкта \n2 - опис проєкта \n3 - список завдань \n4 - список завдань");

        }
        public void EditTask(Project project) //For editing a task
        {
            var num = TaskNum(project);
            var secondOption = "";
            if (project.Tasks[num].Status == "undone")
            {
                secondOption = "done";
            }
            else
            {
                secondOption = "undone";

            }
            Console.WriteLine($"Що саме Ви бажаєте змінити: \n1 - ім'я завдання \n2 - змінити статус на '{secondOption}' \n3 - видалити завдання з проєкту \n4 - відміна");
            switch(Console.ReadLine())
            {
                case "1":
                    Console.Write("Напишіть нове ім'я для завдання: ");
                    project.Tasks[num].Name = Console.ReadLine();
                    break;
                case "2":
                    project.Tasks[num].Status = secondOption; 
                    break;
                case "3": project.Tasks.Remove(project.Tasks[num]); break;
                case "4": break;
                default: Console.WriteLine("Невідома команда, спробуйте ще раз."); break;
            }
        }
        public void EditUser(Project project) //For editing a user
        {
            var num = UserNum(project);
            Console.WriteLine($"Що саме Ви бажаєте змінити: \n1 - ім'я \n2 - позицію користувача \n3 - видалити користувача з проєкту \n4 - відміна");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Напишіть нове ім'я користувача: ");
                    project.Users[num].Name = Console.ReadLine();
                    break;
                case "2":
                    Console.Write("Напишіть нову позицію користувача: ");
                    project.Users[num].Position = Console.ReadLine();
                    break;
                case "3": project.Users.Remove(project.Users[num]); break;
                case "4": break;
                default: Console.WriteLine("Невідома команда, спробуйте ще раз."); break;
            }
        }
        public int ProjectNum(List<Project> projects) //For choosing a project
        {
            PrintList(projects);
            Console.Write("\n Напишіть номер потрібного Вам проєкту: ");
            try
            {
                int num = Convert.ToInt32(Console.ReadLine());
                if (num < projects.Count + 1 && num > 0)
                {
                    return num - 1;
                }
                else
                {
                    Console.WriteLine("Завдання із таким номером не існує, автоматично обрано останній проєкт");
                    return projects.Count - 1;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Трапилася помилка. Можливо, Ви ввели неправильний номер проєкту. Автоматично було обрано перший проєкт");
                return 0;
            }
        }
        public int TaskNum(Project project) //For choosing a task
        {
            PrintList(project.Tasks);
            Console.Write("\n Напишіть номер потрібного Вам завдання: ");
            try
            {
                int num = Convert.ToInt32(Console.ReadLine());
                if(num < project.Tasks.Count + 1 && num > 0)
                {
                    return num - 1;
                }
                else
                {
                    Console.WriteLine("Завдання із таким номером не існує, автоматично обрано останнє завдання в проєкті");
                    return project.Tasks.Count - 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Трапилася помилка. Можливо, Ви ввели неправильний номер завдання. Автоматично було обрано перше завдання");
                return 0;
            }
        }
        public int UserNum(Project project) //For choosing a user
        {
            PrintList(project.Users);
            Console.Write("\n Напишіть номер потрібного Вам користувача: ");
            try
            {
                int num = Convert.ToInt32(Console.ReadLine());
                if (num < project.Users.Count + 1 && num > 0)
                {
                    return num - 1;
                }
                else
                {
                    Console.WriteLine("Завдання із таким номером не існує, автоматично обрано останнього користувача в проєкті");
                    return project.Users.Count - 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Трапилася помилка. Можливо, Ви ввели неправильний номер користувача. Автоматично було обрано першого користувача");
                return 0;
            }
        }
        public void PrintList(List<User> users) //For printing user lists
        {
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{i + 1} * {users[i].Name} ({users[i].Position})");
            }
        }
        public void PrintList(List<Task> tasks) //For printing task lists
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1} * {tasks[i].Name} ({tasks[i].Status})");
            }
        }
        public void PrintList(List<Project> list) //For printing project lists
        {
            for (int i = 0; i < list.Count; i++)
            {
                var users = new StringBuilder();
                for (int j = 0; j < list[i].Users.Count; j++)
                {
                    users.Append(list[i].Users[j].Name + $" ({list[i].Users[j].Position})" + ", ");
                }
                users.Remove(users.Length - 2, 1);
                Console.WriteLine($"{i + 1} * {list[i].Name} - [{users}]");
            }
        }
    }
}
