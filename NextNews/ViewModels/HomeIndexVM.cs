using NextNews.Models;
using NextNews.Models.Database;

namespace NextNews.ViewModels
{
    public class HomeIndexVM
    {
        public List<Article>? MostPopularArticles { get; set; }

        public Article MostPopularArticle { get; set; }

        public List<LatestNewsViewModel>? LatestArticles { get; set; }

        public Article LatestArticle { get; set; }
    }
}
