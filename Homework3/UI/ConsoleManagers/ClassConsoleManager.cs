using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using UI.Interfaces;

namespace UI.ConsoleManagers
{
    public class ClassConsoleManager : ConsoleManager<IClassService, FitnessClass>, IConsoleManager<FitnessClass>
    {
        private readonly TrainerConsoleManager _trainerConsoleManager;
        public ClassConsoleManager(IClassService classService, TrainerConsoleManager trainerConsoleManager) : base(classService)
        {
            _trainerConsoleManager = trainerConsoleManager;
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllClassesAsync },
                { "2", CreateClassAsync },
                { "3", UpdateClassAsync },
                { "4", DeleteClassAsync },
            };

            while (true)
            {
                Console.WriteLine("\nClass operations:");
                Console.WriteLine("1. Display all classes");
                Console.WriteLine("2. Create a new class");
                Console.WriteLine("3. Update a class");
                Console.WriteLine("4. Delete a class");
                Console.WriteLine("5. Exit");

                Console.Write("Enter the operation number: ");
                string input = Console.ReadLine();

                if (input == "5")
                {
                    break;
                }

                if (actions.ContainsKey(input))
                {
                    await actions[input]();
                }
                else
                {
                    Console.WriteLine("Invalid operation number.");
                }
            }
        }
        
        public async Task DisplayAllClassesAsync()
        {
            int i = 1;
            var allClasses = await GetAllAsync();

            foreach (FitnessClass item in allClasses)
            {
                Console.WriteLine($"Class №{i}'s Name: {item.Name}" + Environment.NewLine + $"Trainer: {item.Trainer.FirstName} {item.Trainer.LastName}" + Environment.NewLine + $"Date: {item.Date.ToString("yyyy-MM-dd HH:mm")}");
                i++;
            }
        }

        public async Task CreateClassAsync()
        {
            var fitnessClass = new FitnessClass();
            fitnessClass.Id = Guid.NewGuid();
            await Console.Out.WriteLineAsync("Choose name for your class: ");
            fitnessClass.Name = Console.ReadLine();
            await Console.Out.WriteLineAsync("Choose type of your class: ");
            fitnessClass.Type = Console.ReadLine();
            await Console.Out.WriteLineAsync("Choose trainer (enter number): ");
            await _trainerConsoleManager.DisplayAllTrainersAsync();
            var allTrainers = await _trainerConsoleManager.GetAllTrainers();
            fitnessClass.Trainer = allTrainers[await tryToParse(Console.ReadLine())];
            await Console.Out.WriteLineAsync("Choose date of the class: ");
            string dobString = Console.ReadLine();

            DateTime dob;
            if (DateTime.TryParseExact(dobString, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
            {
                fitnessClass.Date = dob;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
            await CreateAsync(fitnessClass);
        }

        public async Task UpdateClassAsync()
        {
            await DisplayAllClassesAsync();
            await Console.Out.WriteLineAsync("Choose the class to change (enter the number): ");
            var allClasses = (await GetAllAsync()).ToList();
            var fitnessClass = allClasses[await tryToParse(Console.ReadLine())];
            await Console.Out.WriteLineAsync("Choose what to change: " + Environment.NewLine + "1 - name" + Environment.NewLine + "2 - type" + Environment.NewLine + "3 - trainer" + Environment.NewLine + "4 - date of the class");
            switch(Console.ReadLine())
            {
                case "1":
                    await Console.Out.WriteLineAsync("Choose name for your class: ");
                    fitnessClass.Name = Console.ReadLine();
                    break;
                case "2":
                    await Console.Out.WriteLineAsync("Choose type of your class: ");
                    fitnessClass.Type = Console.ReadLine();
                    break;
                case "3":
                    await Console.Out.WriteLineAsync("Choose trainer (enter number): ");
                    await _trainerConsoleManager.DisplayAllTrainersAsync();
                    var allTrainers = await _trainerConsoleManager.GetAllTrainers();
                    fitnessClass.Trainer = allTrainers[await tryToParse(Console.ReadLine())];
                    break;
                case "4":
                    await Console.Out.WriteLineAsync("Choose date of the class (in dd/MM HH:mm format_: ");
                    string dobString = Console.ReadLine();

                    DateTime dob;
                    if (DateTime.TryParseExact(dobString, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                    {
                        fitnessClass.Date = dob;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format.");
                    }
                    break;

            }
        }

        public async Task DeleteClassAsync()
        {
            await DisplayAllClassesAsync();
            await Console.Out.WriteLineAsync("Choose the class to delete (enter the number): ");
            var allClasses = (await GetAllAsync()).ToList();
            await DeleteAsync(allClasses[await tryToParse(Console.ReadLine())].Id);
        }
        public async Task<List<FitnessClass>> GetAllClasses()
        {
            return (await GetAllAsync()).ToList();
        }
    }
}