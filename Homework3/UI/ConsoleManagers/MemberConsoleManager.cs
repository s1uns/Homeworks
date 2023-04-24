using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using UI.Interfaces;

namespace UI.ConsoleManagers
{
    public class MemberConsoleManager : ConsoleManager<IMemberService, Member>, IConsoleManager<Member>
    {
        public MemberConsoleManager(IMemberService memberService) : base(memberService)
        {
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllMembersAsync },
                { "2", AddMemberAsync },
                { "3", UpdateMemberAsync },
                { "4", DeleteMemberAsync },
            };

            while (true)
            {
                Console.WriteLine("\nMember operations:");
                Console.WriteLine("1. Display all members");
                Console.WriteLine("2. Add a new member");
                Console.WriteLine("3. Update a member");
                Console.WriteLine("4. Delete a member");
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
        
        public async Task DisplayAllMembersAsync()
        {
            int i = 1;
            var allMembers = await GetAllAsync();

            foreach (Member item in allMembers)
            {
                Console.WriteLine($"Member №{i}'s full name: {item.FirstName} {item.LastName}" + Environment.NewLine + $"Email: {item.Email}" + Environment.NewLine + $"PhoneNumber: {item.PhoneNumber}" + Environment.NewLine + $"Subscription: {item.SubscriptionType}" + Environment.NewLine + $"Is active: " + (item.IsActive ? "yes." : "no."));
                i++;
            }
        }

        public async Task AddMemberAsync()
        {
            var member = new Member();
            member.Id = Guid.NewGuid();
            await Console.Out.WriteLineAsync("Enter first name for the member: ");
            member.FirstName = Console.ReadLine();
            await Console.Out.WriteLineAsync("Enter last name for the member ");
            member.LastName = Console.ReadLine();
            Console.Write("Enter member's date of birth (DD/MM/YYYY): ");
            string dobString = Console.ReadLine();
            DateTime dob;
            if (DateTime.TryParseExact(dobString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
            {
                member.DateOfBirth = dob;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
            await Console.Out.WriteLineAsync("Enter member's email: ");
            member.Email = Console.ReadLine();
            await Console.Out.WriteLineAsync("Enter member's phone number: ");
            member.PhoneNumber = Console.ReadLine();
            await ChooseSubscription(member);
            member.IsActive = true;
            await CreateAsync(member);
        }

        public async Task UpdateMemberAsync()
        {
            var allMembers = (await GetAllAsync()).ToList();
            await DisplayAllMembersAsync();

            while (true)
            {
                await Console.Out.WriteLineAsync("Enter the number of the trainer: ");
                int num;
                int.TryParse(Console.ReadLine(), out num);
                if (num < allMembers.Count)
                {
                    var member = allMembers[num - 1];
                    await Console.Out.WriteLineAsync("What to change: " + Environment.NewLine + "1 - first mame" + Environment.NewLine + "2 - last name" + "3 - date of birth" + Environment.NewLine + "4 - Email" + Environment.NewLine + "5 - phone number" + Environment.NewLine + "6 - subscription type" + Environment.NewLine + "7 - make the member active/inactive" + Environment.NewLine + "8 - exit");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            await Console.Out.WriteLineAsync("Write new first name for the member: ");
                            member.FirstName = Console.ReadLine();
                            break;
                        case "2":
                            await Console.Out.WriteLineAsync("Write new last name for the member: ");
                            member.LastName = Console.ReadLine();
                            break;
                        case "3":
                            Console.Write("Enter new member's date of birth (DD/MM/YYYY): ");
                            string dobString = Console.ReadLine();
                            DateTime dob;
                            if (DateTime.TryParseExact(dobString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                            {
                                member.DateOfBirth = dob;
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format.");
                            }
                            break;
                        case "4":
                            await Console.Out.WriteLineAsync("Enter new member's email: ");
                            member.Email = Console.ReadLine();
                            break;
                        case "5":
                            await Console.Out.WriteLineAsync("Enter new member's phone number: ");
                            member.PhoneNumber = Console.ReadLine();
                            break;
                        case "6":
                            await ChooseSubscription(member);
                            break;
                        case "7":
                            if (member.IsActive)
                            {
                                member.IsActive = false;
                            }
                            else
                            {
                                member.IsActive = true;
                            }
                            break;

                    }
                    break;
                }
                await Console.Out.WriteLineAsync("Wrong number, try again!");
            }
        }
        public async Task DeleteMemberAsync()
        {
            var allMembers = (await GetAllAsync()).ToList();
            await DisplayAllMembersAsync();
            while (true)
            {
                await Console.Out.WriteLineAsync("Enter the number of the member: ");
                int num;
                int.TryParse(Console.ReadLine(), out num);
                if (num < allMembers.Count)
                {
                    await Service.Delete(allMembers[num - 1].Id);
                    break;
                }
                await Console.Out.WriteLineAsync("Wrong number, try again!");
            }
        }
        public async Task ChooseSubscription(Member member)
        {
            int daysToAdd = 0;
            bool choosing = true;
            while (choosing)
            {
                Console.Out.WriteLineAsync("Choose the type of subscription: " + Environment.NewLine + "1 - Monthly" + Environment.NewLine + "2 - Quarterly" + Environment.NewLine + "3 - Annual");

                switch (Console.ReadLine())
                {
                    case "1":
                        member.SubscriptionType = Core.Enums.SubscriptionType.Monthly; daysToAdd = 30; choosing = false; break;
                    case "2":
                        member.SubscriptionType = Core.Enums.SubscriptionType.Quarterly; daysToAdd = 93; choosing = false; break;
                    case "3":
                        member.SubscriptionType = Core.Enums.SubscriptionType.Annual; daysToAdd = 365; choosing = false; break;
                    default:
                        await Console.Out.WriteLineAsync("Wrong input, try again"); Thread.Sleep(2000); break;
                }
            }
            member.SubscriptionStartDate = DateTime.Now;
            member.SubscriptionEndDate = DateTime.Now.AddDays(daysToAdd);
        }
        public async Task<List<Member>> GetAllMembersBySubscription(string subscriptionType)
        {
            return await Service.GetMembersBySubscriptionType(subscriptionType);
        }
        public async Task<List<Member>> GetAllMembers()
        {
            return (await GetAllAsync()).ToList();
        }
    }
}