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


        public MemberService(IRepository<Member> repository/*, ISubscriptionService subscriptionService, IClassService classService*/) : base(repository)
        {
            /*_classService = classService;*/
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
            var allMembers = await GetAll();
            return allMembers.Where(x => x.SubscriptionStartDate ==  startDate && x.SubscriptionEndDate == endDate).ToList();
        }
    }
}