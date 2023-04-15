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
    public class BookingConsoleManager : ConsoleManager<IBookingService, Booking>, IConsoleManager<Booking>
    {
        private readonly ClassConsoleManager _classConsoleManager;
        private readonly MemberConsoleManager _memberConsoleManager;
        
        public BookingConsoleManager(IBookingService bookingService, ClassConsoleManager classConsoleManager, MemberConsoleManager memberConsoleManager)
            : base(bookingService)
        {
            _classConsoleManager = classConsoleManager;
            _memberConsoleManager = memberConsoleManager;
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllBookingsAsync },
                { "2", CreateBookingAsync },
                { "3", UpdateBookingAsync },
                { "4", DeleteBookingAsync },
            };

            while (true)
            {
                Console.WriteLine("\nBooking operations:");
                Console.WriteLine("1. Display all bookings");
                Console.WriteLine("2. Create a new booking");
                Console.WriteLine("3. Update a booking");
                Console.WriteLine("4. Delete a booking");
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

        public async Task DisplayAllBookingsAsync()
        {
            int i = 1;
            var allBookings = await GetAllAsync();
            foreach (Booking item in allBookings)
            {
                Console.WriteLine($"Booking №{i}:" + Environment.NewLine + $"Member: {item.Member.FirstName} {item.Member.LastName}" + Environment.NewLine + $"Class: {item.Class.Name}" + Environment.NewLine + $"Date: {item.Date}" + $"Is confirmed: " + (item.IsConfirmed ? "yes." : "no.") );
                i++;
            }
        }

        public async Task CreateBookingAsync()
        {
            var booking = new Booking();
            booking.Id = Guid.NewGuid();
            await Console.Out.WriteLineAsync("Choose member (enter number): ");
            await _memberConsoleManager.DisplayAllMembersAsync();
            var allMembers = await _memberConsoleManager.GetAllMembers();
            booking.Member = allMembers[Convert.ToInt32(Console.ReadLine()) - 1];
            await Console.Out.WriteLineAsync("Choose class (enter number): ");
            await _memberConsoleManager.DisplayAllMembersAsync();
            var allClasses = await _classConsoleManager.GetAllClasses();
            booking.Class = allClasses[Convert.ToInt32(Console.ReadLine()) - 1];
            await Console.Out.WriteLineAsync("Choose date of the booking (in dd/MM HH:mm format): ");
            string dobString = Console.ReadLine();

            DateTime dob;
            if (DateTime.TryParseExact(dobString, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
            {
                booking.Date = dob;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
            booking.IsConfirmed = true;
            await CreateAsync(booking);
        }

        public async Task UpdateBookingAsync()
        {
            await DisplayAllBookingsAsync();
            await Console.Out.WriteLineAsync("Choose the class to delete (enter the number): ");
            var allBookings = await GetAllAsync();
            var bookingsList = allBookings.ToList();
            var booking = bookingsList[Convert.ToInt32(Console.ReadLine()) - 1];
            await Console.Out.WriteLineAsync("Choose member (enter number): ");
            await _memberConsoleManager.DisplayAllMembersAsync();
            var allMembers = await _memberConsoleManager.GetAllMembers();
            booking.Member = allMembers[Convert.ToInt32(Console.ReadLine()) - 1];
            await Console.Out.WriteLineAsync("Choose class (enter number): ");
            await _classConsoleManager.DisplayAllClassesAsync();
            var allClasses = await _classConsoleManager.GetAllClasses();
            booking.Class = allClasses[Convert.ToInt32(Console.ReadLine()) - 1];
            await Console.Out.WriteLineAsync("Choose date of the booking (in dd/MM HH:mm format): ");
            string dobString = Console.ReadLine();

            DateTime dob;
            if (DateTime.TryParseExact(dobString, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
            {
                booking.Date = dob;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
            booking.IsConfirmed = true;
        }

        public async Task DeleteBookingAsync()
        {
            await DisplayAllBookingsAsync();
            await Console.Out.WriteLineAsync("Choose the class to delete (enter the number): ");
            var allBookings = await GetAllAsync();
            var bookingsList = allBookings.ToList();
            await DeleteAsync(bookingsList[Convert.ToInt32(Console.ReadLine()) - 1].Id);
        }
    }
}