using System.ComponentModel.DataAnnotations;

namespace NextNews.Models.Database
{
    public class Subscription
    {
        public int Id { get; set; }

        public SubscriptionType? SubscriptionType { get; set; }

        public decimal? Price { get; set; }

        public DateTime? Created { get; set;}

        public DateTime? Expired { get; set;}

        public string? PaymentComplete { get; set;}

        public int UserId { get; set;}

        //Foreign Key
        public virtual User? User { get; set; }
    
    }
}
