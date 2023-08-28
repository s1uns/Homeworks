using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using UI.Interfaces;

namespace UI.ConsoleManagers
{
    public class SubscriptionConsoleManager : ConsoleManager<ISubscriptionService, Subscription>, IConsoleManager<Subscription>
    {
        private readonly MemberConsoleManager _memberConsoleManager;
        public SubscriptionConsoleManager(ISubscriptionService subscriptionService, MemberConsoleManager memberConsoleManager) : base(subscriptionService)
        {
            _memberConsoleManager = memberConsoleManager;
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllSubscriptionsAsync },
                { "2", CreateSubscriptionAsync },
                { "3", UpdateSubscriptionAsync },
                { "4", DeleteSubscriptionAsync },
            };

            while (true)
            {
                Console.WriteLine("\nSubscription operations:");
                Console.WriteLine("1. Display all subscriptions");
                Console.WriteLine("2. Create a new subscription");
                Console.WriteLine("3. Update a subscription");
                Console.WriteLine("4. Delete a subscription");
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

        public async Task DisplayAllSubscriptionsAsync()
        {
            int i = 1;
            var allMembers = await GetAllAsync();

            foreach (Subscription item in allMembers)
            {
                Console.WriteLine($"Subscription №{i}'s member: {item.Member.FirstName} {item.Member.LastName}" + Environment.NewLine + $"Subscription type: {item.Type}" + Environment.NewLine + "Is active: " + (item.IsActive ? "yes." : "no."));
                i++;
            }
        }

        public async Task CreateSubscriptionAsync()
        {
            var subscription = new Subscription();
            subscription.Id = Guid.NewGuid();
            bool choosing = true;
            int daysToAdd = 0;

            while (choosing)
            {
                await Console.Out.WriteLineAsync("Choose type of subscription: " + Environment.NewLine + "1 - Monthly" + Environment.NewLine + "2 - Quarterly" + Environment.NewLine + "3 - Annual");

                switch (Console.ReadLine())
                {
                    case "1":
                        subscription.Type = Core.Enums.SubscriptionType.Monthly; daysToAdd = 31; choosing = false; break;
                    case "2":
                        subscription.Type = Core.Enums.SubscriptionType.Quarterly; daysToAdd = 93; choosing = false; break;
                    case "3":
                        subscription.Type = Core.Enums.SubscriptionType.Annual; daysToAdd = 365; choosing = false; break;
                    default:
                        await Console.Out.WriteLineAsync("Wrong input, try again"); Thread.Sleep(2000); break;

                }
            }
            await Console.Out.WriteLineAsync("Choose member to add to subscription (enter his number): ");
            var membersForChoosing = await _memberConsoleManager.GetAllMembersBySubscription(subscription.Type.ToString());
            int i = 0;

            foreach(Member item in membersForChoosing)
            {
                Console.WriteLine($"№{1}: {item.FirstName} {item.LastName} ({item.Email})");
                i++;
            }
            subscription.Member = membersForChoosing[await tryToParse(Console.ReadLine())];
            subscription.StartDate = DateTime.Now;
            subscription.EndDate = DateTime.Now.AddDays(daysToAdd);
            subscription.IsActive = true;
            await CreateAsync(subscription);

        }

        public async Task UpdateSubscriptionAsync()
        {
            var allSubscriptions = (await GetAllAsync()).ToList();
            await Console.Out.WriteLineAsync("Choose subscription to delete (enter the number): ");
            var subscription = allSubscriptions[await tryToParse(Console.ReadLine())];
            await Console.Out.WriteLineAsync("What to change: " + Environment.NewLine + "1 - TypeOfSubscription" + Environment.NewLine + "2 - renew the subscription" + Environment.NewLine + "3 - make the subscription active/inactive" + Environment.NewLine + "4 - exit");

            switch (Console.ReadLine())
            {
                case "1":
                    bool choosing = true;

                    while (choosing)
                    {
                        await Console.Out.WriteLineAsync("Choose type of subscription: " + Environment.NewLine + "1 - Monthly" + Environment.NewLine + "2 - Quarterly" + Environment.NewLine + "3 - Annual");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                subscription.Type = Core.Enums.SubscriptionType.Monthly; choosing = false; break;
                            case "2":
                                subscription.Type = Core.Enums.SubscriptionType.Quarterly; choosing = false; break;
                            case "3":
                                subscription.Type = Core.Enums.SubscriptionType.Annual; choosing = false; break;
                            default:
                                await Console.Out.WriteLineAsync("Wrong input, try again"); Thread.Sleep(2000); break;

                        }
                    }
                    break;
                case "2":
                    await Service.RenewSubscription(subscription.Id);
                    break;
                case "3":
                    if(subscription.IsActive)
                    {
                        subscription.IsActive = false; 
                    }
                    else
                    {
                        subscription.IsActive = true;
                    }
                    break;
                case "4":
                    break;
            }
        }

        public async Task DeleteSubscriptionAsync()
        {
            var allSubscriptions = (await GetAllAsync()).ToList();
            await Console.Out.WriteLineAsync("Choose subscription to delete (enter the number): ");
            await DisplayAllSubscriptionsAsync();
            await DeleteAsync(allSubscriptions[await tryToParse(Console.ReadLine())].Id);
        }
    }
}