using NextNews.Models.Database;

namespace NextNews.Services
{
    public interface ISubscriptionService
    {
        public Task<List<Subscription>> GetSubscriptionsAsync();
        public Task CreateSubscriptionAsync(Subscription subscription);
        public Task<Subscription> GetSubscriptionByIdAsync(int id);
        public Task UpdateSubscriptionAsync(Subscription subscription);
        public Task DeleteSubscriptionAsync(int id);
        public Task<List<SubscriptionType>> GetSubscriptionTypesAsync();
        public Task CreateSubscriptionTypesAsync(SubscriptionType subscriptionType);
        public void CreateSubscriptionForUser(string userId, int subscriptionTypeId);
        public Task<SubscriptionType> GetSubscriptionTypeByIdAsync(int id);
        public Task UpdateSubscriptionTypeAsync(SubscriptionType subscriptionType);
        public Task DeleteSubscriptionType(int id);


        //count of Subscribers type
        public Task<int> CountBasicSubscribersAsync();


        public Task<int> CountPremiumSubscribersAsync();

    }
}
