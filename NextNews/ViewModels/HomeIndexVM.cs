using NextNews.Models;
using NextNews.Models.Database;

namespace NextNews.ViewModels
{
    public class HomeIndexVM
    {
        public List<Article>? PopularArticles { get; set; }

        public Article PopularArticle { get; set; }

        public List<Article>? LatestArticles { get; set; }

        public Article LatestArticle { get; set; }
    }
}
