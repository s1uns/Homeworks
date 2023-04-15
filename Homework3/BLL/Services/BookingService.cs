using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class BookingService : GenericService<Booking>, IBookingService
    {
        private readonly IClassService _classService;
        private readonly IMemberService _memberService;

        public BookingService(IRepository<Booking> repository, IClassService classService, IMemberService memberService)
            : base(repository)
        {
            _classService = classService;
            _memberService = memberService;
        }

        public async Task<Booking> BookClass(Guid memberId, Guid classId)
        {   var booking = new Booking();
            var member = await _memberService.GetById(memberId);
            var fitnessClass = await _classService.GetById(classId);
            booking.Member = member;
            booking.Class = fitnessClass;
            booking.Date = DateTime.Now;
            booking.IsConfirmed = false;
            await Add(booking);
            return booking;
        }

        public async Task<List<Booking>> GetBookingsByMember(Guid memberId)
        {
            var allBookings = await GetAll();
            return allBookings.Where(x => x.Member.Id  == memberId).ToList();
        }

        public async Task<List<Booking>> GetBookingsByClass(Guid classId)
        {
            var allBookings = await GetAll();
            return allBookings.Where(x => x.Class.Id == classId).ToList();
        }

        public async Task<List<Booking>> GetBookingsByDate(DateTime date)
        {
            var allBookings = await GetAll();
            return allBookings.Where(x => x.Date == date).ToList();
        }

        public async Task ConfirmBooking(Guid bookingId)
        {
            var booking = await GetById(bookingId);
            booking.IsConfirmed = true;
        }
    }
}