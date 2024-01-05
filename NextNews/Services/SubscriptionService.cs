using NextNews.Data;

namespace NextNews.Services
{
    public class SubscriptionService :ISubscriptionService
    {
        private readonly ApplicationDbContext _context;
        public SubscriptionService(ApplicationDbContext context) 
        {
           _context = context;
        }


    }
}
