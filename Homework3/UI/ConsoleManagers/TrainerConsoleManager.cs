using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using UI.Interfaces;

namespace UI.ConsoleManagers
{
    public class TrainerConsoleManager : ConsoleManager<ITrainerService, Trainer>, IConsoleManager<Trainer>
    {
        public TrainerConsoleManager(ITrainerService trainerService) : base(trainerService)
        {
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllTrainersAsync },
                { "2", CreateTrainerAsync },
                { "3", UpdateTrainerAsync },
                { "4", DeleteTrainerAsync },
            };

            while (true)
            {
                Console.WriteLine("\nTrainer operations:");
                Console.WriteLine("1. Display all trainers");
                Console.WriteLine("2. Create a new trainer");
                Console.WriteLine("3. Update a trainer");
                Console.WriteLine("4. Delete a trainer");
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

        public async Task DisplayAllTrainersAsync()
        {
            int i = 1;
            var allTrainers = await GetAllAsync();

            foreach (Trainer item in allTrainers)
            {
                Console.WriteLine($"Trainer №{i}'s full name: {item.FirstName} {item.LastName}" + Environment.NewLine + $"Specialization: {item.Specialization}");
                i++;
            }
        }

        public async Task CreateTrainerAsync()
        {
            var trainer = new Trainer();
            trainer.Id = Guid.NewGuid();
            await Console.Out.WriteLineAsync("Enter first name for the user: ");
            trainer.FirstName = Console.ReadLine();
            await Console.Out.WriteLineAsync("Enter last name for the user: ");
            trainer.LastName = Console.ReadLine();
            await Console.Out.WriteLineAsync("Enter specialization for the user: ");
            trainer.Specialization = Console.ReadLine();
            await CreateAsync(trainer);
        }

        public async Task UpdateTrainerAsync()
        {
            var allTrainers = (await GetAllAsync()).ToList();
            await DisplayAllTrainersAsync();

            while (true)
            {
                await Console.Out.WriteLineAsync("Enter the number of the trainer: ");
                var num = await tryToParse(Console.ReadLine());
                if (num < allTrainers.Count)
                {   var trainer = allTrainers[num - 1];
                    await Console.Out.WriteLineAsync("What to change: " + Environment.NewLine + "1 - first mame" + Environment.NewLine + "2 - last name" + "3 - specialization" + Environment.NewLine + "4 - exit");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            await Console.Out.WriteLineAsync("Write new first name for the trainer: ");
                            trainer.FirstName = Console.ReadLine();
                            break;
                        case "2":
                            await Console.Out.WriteLineAsync("Write new last name for the trainer: ");
                            trainer.LastName = Console.ReadLine();
                            break;
                        case "3":
                            await Console.Out.WriteLineAsync("Write new specialization for the trainer: ");
                            trainer.Specialization = Console.ReadLine();
                            break;
                        case "4":
                            break;

                    }
                    break;
                }
                await Console.Out.WriteLineAsync("Wrong number, try again!");
            }
        }

        public async Task DeleteTrainerAsync()
        {
            var allTrainers = (await GetAllAsync()).ToList();
            await DisplayAllTrainersAsync();
            while (true)
            {
                await Console.Out.WriteLineAsync("Enter the number of the trainer: ");
                var num = await tryToParse(Console.ReadLine());
                if (num < allTrainers.Count)
                {
                    await Service.Delete(allTrainers[num - 1].Id);
                    break;
                }
                await Console.Out.WriteLineAsync("Wrong number, try again!");
            }
        }
        public async Task<List<Trainer>> GetAllTrainers()
        {
            return (await GetAllAsync()).ToList();
        }
    }
}