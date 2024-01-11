namespace NextNews.Models
{
    public class PopularNewsViewModel
    {
        public int Id { get; set; }
        public string? HeadLine { get; set; }
        public string? ContentSummary { get; set; }
        public DateTime? DateStamp { get; set; }

        public string ImageLink { get; set; }

    }
}
