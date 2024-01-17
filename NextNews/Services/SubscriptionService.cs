using MailKit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextNews.Data;
using NextNews.Models.Database;

namespace NextNews.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _context;
        public SubscriptionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Subscription>> GetSubscriptionsAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        //create 
        public async Task CreateSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
        }
        //details subscription
        public async Task<Subscription> GetSubscriptionByIdAsync(int id)
        {
            return await _context.Subscriptions.FindAsync(id);
        }
        //Update subscription
        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }

        //delete Subscription

        public async Task DeleteSubscriptionAsync(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }

        //Get Subscription Types
        public async Task<List<SubscriptionType>> GetSubscriptionTypesAsync()
        {
            return await _context.SubscriptionTypes.ToListAsync();
        }
        //Create Subscription Types
        public async Task CreateSubscriptionTypesAsync(SubscriptionType subscriptionType)
        {
            _context.SubscriptionTypes.Add(subscriptionType);
            await _context.SaveChangesAsync();
        }
        //Details SubscriptionType 
        public async Task<SubscriptionType> GetSubscriptionTypeByIdAsync(int id)
        {
            return await _context.SubscriptionTypes.FindAsync(id);
        }

        //Update/edit Subscription Type

        public async Task UpdateSubscriptionTypeAsync(SubscriptionType subscriptionType)
        {
            _context.SubscriptionTypes.Update(subscriptionType);
            await _context.SaveChangesAsync();
        }



        //Create subscription for user
        public void CreateSubscriptionForUser(string userId, int subscriptionTypeId)
        {
            var subscriptionType = _context.SubscriptionTypes.FirstOrDefault(st => st.Id == subscriptionTypeId);
            if (subscriptionType == null)
            {
                throw new ArgumentException("Invalid Subscription Type ID");
            }
            //var existingSubscription = _context.Subscriptions.FirstOrDefault(x => x.UserId == userId && x.SubscriptionTypeId == subscriptionTypeId);
            //if (existingSubscription != null) 
            //{

            //    throw new ArgumentException("You already using this plan");
            //}
            var subscription = new Subscription()
            {

                UserId = userId,
                SubscriptionTypeId = subscriptionTypeId,
                Price = subscriptionType.Price,
                Created = DateTime.Now,
                Expired = DateTime.Now.AddMonths(1),
                PaymentComplete = "No"
            };
            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

        }
        public async Task DeleteSubscriptionType(int id)
        {
            var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);
            if (subscriptionType != null)
            {
                _context.SubscriptionTypes.Remove(subscriptionType);
                await _context.SaveChangesAsync();
            }
        }


       

        public async Task<int> CountBasicSubscribersAsync()
        {
            return await _context.Subscriptions
                .Where(subscription => subscription.SubscriptionType.Name == "Basic")
                .CountAsync();
        }

       

        public async Task<int> CountPremiumSubscribersAsync()
        {
            return await _context.Subscriptions
                .Where(subscription => subscription.SubscriptionType.Name == "Premium")
                .CountAsync();
        }


    }
}