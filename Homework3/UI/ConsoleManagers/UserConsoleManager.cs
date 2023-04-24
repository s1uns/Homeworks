using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using UI.Interfaces;
using System.Security.Cryptography;
using System.Reflection;
using System.Transactions;
using System.Linq;

namespace UI.ConsoleManagers
{
    public class UserConsoleManager : ConsoleManager<IUserService, User>, IConsoleManager<User>
    {
        public UserConsoleManager(IUserService userService) : base(userService)
        {
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllUsersAsync },
                { "2", CreateUserAsync },
                { "3", UpdateUserAsync },
                { "4", DeleteUserAsync },
            };

            while (true)
            {
                Console.WriteLine("\nUser operations:");
                Console.WriteLine("1. Display all users");
                Console.WriteLine("2. Create a new user");
                Console.WriteLine("3. Update a user");
                Console.WriteLine("4. Delete a user");
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

        public async Task DisplayAllUsersAsync()
        {
            int i = 1;
            var allUsers = await GetAllAsync();
            foreach (User item in allUsers)
            {
                Console.WriteLine($"User №{i}'s username: {item.Username}" + Environment.NewLine + $"Role: {item.Role}" + Environment.NewLine + $"Is locked: " + (item.IsLocked ? "yes." : "no."));
                i++;
            }
        }

        public async Task CreateUserAsync()
        {
            var user = new User();
            user.Id = Guid.NewGuid();
            await Console.Out.WriteLineAsync("Enter username for the user: ");
            user.Username = Console.ReadLine();
            await Console.Out.WriteLineAsync("Create a password: ");
            user.PasswordHash = Hash(Console.ReadLine());
            bool choosing = true;

            while (choosing)
            {
                await Console.Out.WriteLineAsync("Choose the role: " + Environment.NewLine + "1 - member" + Environment.NewLine + "2 - trainer");

                switch (Console.ReadLine())
                {
                    case "1":
                        user.Role = Core.Enums.UserRole.Member; choosing = false;  break;
                    case "2":
                        user.Role = Core.Enums.UserRole.Trainer; choosing = false; break;
                    default:
                        await Console.Out.WriteLineAsync("Wrong input, try again"); Thread.Sleep(2000); break;

                }
            }
            user.IsLocked = false;
            await CreateAsync(user);

        }

        public async Task UpdateUserAsync()
        {
            var allUsers = await GetAllAsync();
            var user = new User();
            await DisplayAllUsersAsync();
            while (true)
            {
                await Console.Out.WriteLineAsync("Enter the username of the user: ");
                var username = Console.ReadLine();
                user = allUsers.Where(x => x.Username == username).First();
                if (user != null)
                {
                    break;
                }
                await Console.Out.WriteLineAsync("Wrong username, try again!");
            }

            var choosing = true;
            while (choosing)
            {
                await Console.Out.WriteLineAsync("What u wanna change?" + Environment.NewLine + "1 - username" + Environment.NewLine + "2 - password" + Environment.NewLine + "3 - role" + Environment.NewLine + "4 - lock/unlock user" + Environment.NewLine + "5 - exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        await Console.Out.WriteLineAsync("Enter new username: ");
                        user.Username = Console.ReadLine(); break;
                    case "2":
                        await Console.Out.WriteLineAsync("Enter the old password: ");
                        if (user.PasswordHash == Hash(Console.ReadLine()))
                        {
                            await Console.Out.WriteAsync("Enter the new password: ");
                            await Service.ResetPassword(user.Id, Console.ReadLine());
                            return;
                        }
                        else
                        {
                            throw new Exception($"You entered the wrong password :(");
                        }
                        break;
                    case "3":
                        await Console.Out.WriteLineAsync("Choose the role: " + Environment.NewLine + "1 - Admin" + Environment.NewLine + "2 - Trainer" + Environment.NewLine + "3 - Member" + Environment.NewLine + "4 - exit");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                user.Role = Core.Enums.UserRole.Admin; break;
                            case "2":
                                user.Role = Core.Enums.UserRole.Trainer; break;
                            case "3":
                                user.Role = Core.Enums.UserRole.Member; break;
                            case "4": break;
                        }
                        break;
                    case "4":
                        if (user.IsLocked)
                        {
                            Service.UnlockUser(user.Id);
                        }
                        else
                        {
                            Service.LockUser(user.Id);
                        }
                        break;
                    default: Console.WriteLine("Wrong input."); break;
                    case "5":
                        break;
                }
            }
        }

        public async Task DeleteUserAsync()
        {
            var allUsers = await GetAllAsync();
            var user = new User();
            await DisplayAllUsersAsync();
            while (true)
            {
                await Console.Out.WriteLineAsync("Enter the username of the user: ");
                var username = Console.ReadLine();
                user = allUsers.Where(x => x.Username == username).First();
                if (user != null)
                {
                    break;
                }
                await Console.Out.WriteLineAsync("Wrong username, try again!");
            }
            Service.Delete(user.Id);
        }
        public string Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}