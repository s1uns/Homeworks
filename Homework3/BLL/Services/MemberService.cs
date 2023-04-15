using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class MemberService : GenericService<Member>, IMemberService
    {
        private readonly IClassService _classService;


        public MemberService(IRepository<Member> repository, ISubscriptionService subscriptionService, IClassService classService) : base(repository)
        {
            _classService = classService;
        }

        public async Task<Member> RegisterMember(Member member)
        {
            await Add(member);
            return member;
        }

        public async Task<List<Member>> GetActiveMembers()
        {
            var allMembers = await GetAll();
            return allMembers.Where(x => x.IsActive).ToList();

        }

        public async Task<List<Member>> GetMembersBySubscriptionType(string subscriptionType)
        {
            var allMembers = await GetAll();
            return allMembers.Where(x => x.SubscriptionType.ToString() == subscriptionType).ToList();
        }

        public async Task<List<Member>> GetMembersWithUpcomingRenewal(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckMemberAttendance(Guid memberId, DateTime date)
        {
            var allClasses = await _classService.GetAll();
            var searchedClass = allClasses.Where(x => x.Date == date).First();
            var member = await GetById(memberId);
            return searchedClass.Attendees.Contains(member);

        }

        public async Task RecordMemberAttendance(Guid memberId, DateTime date)
        {
            {
                var allClasses = await _classService.GetAll();
                var searchedClass = allClasses.Where(x => x.Date == date).First();
                var member = await GetById(memberId);
                searchedClass.Attendees.Add(member);
            }
        }
    }
}