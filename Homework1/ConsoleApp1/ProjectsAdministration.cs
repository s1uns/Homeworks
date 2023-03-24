using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    internal class ProjectsAdministration
    {
        public List<Project> Projects { get; }
        public List<User> Users { get; }

        public void AddProject(List<Project> projects)
        {
            Console.Write("Вкажіть назву проєкту: ");
            var name = Console.ReadLine();
            Console.Write("Напишіть невеликий опис до проєкту: ");
            var description = Console.ReadLine();
            Console.Write("Виділіть кількість днів на виконання проєкту: ");
            var dateOfTerm = CreateDateOfTerm(Convert.ToInt32(Console.ReadLine()));
            Console.Write("Вкажіть бюджет, виділенний на реалізацію проєкту: ");
            var budget = Console.ReadLine();
            projects.Add(new Project(name, description, null, dateOfTerm, budget));
            Console.WriteLine("Проєкт успішно створено!");

        }
        public void AddTask(Project project)
        {
            Console.Write("Вкажіть назву завдання: ");
            var name = Console.ReadLine();
            project.Tasks.Add(new Task(name, "undone", null));
            Console.WriteLine("Завдання успішно додано до проєкту!");
        }
        public void AddUser(List<User> users)
        {
            Console.Write("Вкажіть ім'я користувача: ");
            var name = Console.ReadLine();
            Console.Write("Вкажіть посаду користувача: ");
            var position = Console.ReadLine();
            users.Add(new User(name, position));
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
        public void EditProject(List<Project> projects)
        {   var keep = true;
            var projectNum = ProjectNum(projects);
            while (keep) { 
                Console.WriteLine($"Інформація про проєкт: \nНазва - {projects[projectNum].Name} \nОпис - {projects[projectNum].Description} \nСписок завдань:");
                PrintList(projects[projectNum].Tasks);
                Console.WriteLine($"Термін, до якого проєкт слід виконати: {projects[projectNum].Term.ToString("dd MMM yyyy")} ({DateDifference(projects[projectNum].Term)} днів) \nБюджет: {projects[projectNum].Budget} $ \nЗавершеність: {ProjectCompleteness(projects[projectNum]).ToString("0.00")} %");
                Console.WriteLine($"Що саме Ви бажаєте змінити: \n1 - ім'я проєкта \n2 - опис проєкта \n3 - додати завдання \n4 - список завдань \n5 - збільшити термін виконання \n6 - бюджет \n7 - видалити проєкт \n8 - відміна");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Оберіть нове ім'я для проєкту: ");
                        projects[projectNum].Name = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Напишіть новий опис проєкту: ");
                        projects[projectNum].Description = Console.ReadLine();
                        break;
                    case "3":
                        AddTask(projects[projectNum]);
                        break;
                    case "4":
                        EditTask(projects[projectNum], Users);
                        break;
                    case "5":
                        Console.Write("Скільки днів бажаєте додати до існуючої дати: ");
                        try
                        {
                            var daysToAdd = Convert.ToInt32(Console.ReadLine());
                            projects[projectNum].Term = projects[projectNum].Term.AddDays(daysToAdd);
                        }
                        catch
                        {
                            Console.WriteLine("Ви неправильно вказали кількість днів, спробуйте ще раз.");
                        }
                        break;
                    case "6":
                        Console.Write("Вкажіть поточний бюджет даного проєкту: ");
                        projects[projectNum].Budget = Console.ReadLine();
                        break;
                    case "7": projects.Remove(projects[projectNum]); keep = false; break;
                    case "8": keep = false; break;
                    default: Console.WriteLine("Невідома команда, спробуйте ще раз."); break;

                }
            }
        }
        public void EditTask(Project project, List<User> freeusers) //For editing a task
        {
            var keep = true;
            var taskNum = TaskNum(project);
            while (keep)
            {
                Console.WriteLine($"Інформація про завдання: \nНазва - {project.Tasks[taskNum].Name} \nСтатус - {project.Tasks[taskNum].Status} \nКористувачі:");
                PrintList(project.Tasks[taskNum].Users);
                var secondOption = "";
                if (project.Tasks[taskNum].Status == "undone")
                {
                    secondOption = "done";
                }
                else
                {
                    secondOption = "undone";

                }
                Console.WriteLine($"Що саме Ви бажаєте змінити: \n1 - ім'я завдання \n2 - змінити статус на '{secondOption}' \n3 - видалити завдання з проєкту \n4 - редагувати користувачів \n5 - додати користувача \n6 - відміна");
                switch(Console.ReadLine())
                {
                    case "1":
                        Console.Write("Напишіть нове ім'я для завдання: ");
                        project.Tasks[taskNum].Name = Console.ReadLine();
                        break;
                    case "2":
                        project.Tasks[taskNum].Status = secondOption; 
                        break;
                    case "3": project.Tasks.Remove(project.Tasks[taskNum]); break;
                    case "4":
                        Console.WriteLine("Список призначених до цієї задачі користувачів: ");
                        var userNum = UserNum(project.Tasks[taskNum].Users);
                        Console.WriteLine("Що робити з користувачем? \n1-прибрати з завдання \n2-вихід");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                freeusers.Add(project.Tasks[taskNum].Users[userNum]);
                                project.Tasks[taskNum].Users.Remove(project.Tasks[taskNum].Users[userNum]);
                                Console.WriteLine("Користувача прибрано з завдання");
                                break;
                            case "2":
                                break;
                            default: Console.WriteLine("Невідома команда, спробуйте ще раз."); break;


                        }
                        break;
                    case "5": 
                        var freeUserNum = UserNum(freeusers);
                        project.Tasks[taskNum].Users.Add(freeusers[freeUserNum]);
                        freeusers.Remove(freeusers[freeUserNum]);
                        break;
                    case "6": keep = false; break;
                    default: Console.WriteLine("Невідома команда, спробуйте ще раз."); break;
                }
            }
        }





        public void EditUser(List<User> users) //For editing a user
        {   var keep = true;
            var num = UserNum(users);
            while (keep) { 
                Console.WriteLine($"Інформація про користувача: \nІм'я - {users[num].Name} \nПосада - {users[num].Position}" );
                Console.WriteLine($"Що саме Ви бажаєте змінити: \n1 - ім'я \n2 - позицію користувача \n3 - видалити користувача з проєкту \n4 - відміна");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Напишіть нове ім'я користувача: ");
                        users[num].Name = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Напишіть нову позицію користувача: ");
                        users[num].Position = Console.ReadLine();
                        break;
                    case "3": users.Remove(users[num]); break;
                    case "4": keep = false; break;
                    default: Console.WriteLine("Невідома команда, спробуйте ще раз."); break;
                }
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
        public int UserNum(List <User> user) //For choosing a user
        {   
            PrintList(user);
            Console.Write("\n Напишіть номер потрібного Вам користувача: ");
            try
            {
                int num = Convert.ToInt32(Console.ReadLine());
                if (num < Users.Count + 1 && num > 0)
                {
                    return num - 1;
                }
                else
                {
                    Console.WriteLine("Користувача із таким номером не існує, автоматично обрано останнього користувача в проєкті");
                    return Users.Count - 1;
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
            if (users.Count == 0)
            {
                Console.WriteLine("Наразі список користувачів цього завдання пустий."); return;
            }
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{i + 1} * {users[i].Name} ({users[i].Position})");
            }
        }
        public void PrintList(List<Task> tasks) //For printing task lists
        { if (tasks.Count == 0)
            {
                Console.WriteLine("Наразі список завдань цього проєкту пустий."); return; 
            }
          for (int i = 0; i < tasks.Count; i++)
          {
                Console.WriteLine($"{i + 1} * {tasks[i].Name} ({tasks[i].Status})");
          }
        }
        public void PrintList(List<Project> projects) //For printing project lists
        {
            if (projects.Count == 0)
            {
                Console.WriteLine("Наразі список проєктів пустий."); return;
            }
            for (int i = 0; i < projects.Count; i++)
            {
                Console.WriteLine($"{i + 1} * {projects[i].Name} - {ProjectCompleteness(projects[i]).ToString("0.00")} %");
            }
        }
        public int DateDifference(DateTime d1)
        {
            TimeSpan difference = d1.Subtract(DateTime.Today);
            return (int)difference.TotalDays;
        }
        public double ProjectCompleteness(Project project)
        {   
            int overall = project.Tasks.Count;
            if (overall == 0) return 0;
            int finished = 0;
            foreach(Task task in project.Tasks)
            {
                if (task.Status == "done") finished++;
            }
            double result = ((double)finished / overall) * 100.0;
            return result;
        }
        public ProjectsAdministration(List<Project> projects, List<User> users)
        {
            Projects = projects ?? new List<Project>();
            Users = users ?? new List<User>();
        }
    }
}
