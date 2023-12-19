using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NextNews.Models.Database;

namespace NextNews.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

           
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<SubscriptionType> SubscriptionTypes { get; set; } 
    }
}
