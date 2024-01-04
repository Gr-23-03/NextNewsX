using NextNews.Models;
using NextNews.Models.Database;

namespace NextNews.ViewModels
{
    public class HomeIndexVM
    {
        public List<Article>? MostPopularArticles { get; set; }

        public List<LatestNewsViewModel>? LatestArticles { get; set; }
    }
}
