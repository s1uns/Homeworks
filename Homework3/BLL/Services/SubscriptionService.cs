using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace BLL.Services
{
    public class SubscriptionService : GenericService<Subscription>, ISubscriptionService
    {
        public SubscriptionService(IRepository<Subscription> repository) : base(repository)
        {
        }

        public async Task<Subscription> CreateSubscription(Subscription subscription)
        {
            await Add(subscription);
            return subscription;
        }

        public async Task<List<Subscription>> GetSubscriptionsByMember(Guid memberId)
        {
            var allSubscriptions = await GetAll();
            return allSubscriptions.Where(x => x.Member.Id == memberId).ToList();
        }

        public async Task<List<Subscription>> GetSubscriptionsByType(string subscriptionType)
        {
            var allSubscriptions = await GetAll();
            return allSubscriptions.Where(x => x.Type.ToString() == subscriptionType).ToList();
        }

        public async Task RenewSubscription(Guid subscriptionId)
        {
            var subscription = await GetById(subscriptionId);
            int DaysToAdd = 0;

            switch ((int)subscription.Type)
            {
                case 0:
                    DaysToAdd = 31; break;
                case 1:
                    DaysToAdd = 93; break;
                case 2:
                    DaysToAdd = 365; break;
            }
            subscription.StartDate = DateTime.Now;
            subscription.EndDate = DateTime.Now.AddDays(DaysToAdd);
        }

        public async Task CancelSubscription(Guid subscriptionId)
        {
            Delete(subscriptionId);
        }
    }
}