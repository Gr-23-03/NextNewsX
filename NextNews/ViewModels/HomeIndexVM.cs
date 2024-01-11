using NextNews.Models;
using NextNews.Models.Database;

namespace NextNews.ViewModels
{
    public class HomeIndexVM
    {
        public List<Article>? PopularArticles { get; set; }

        public List<PopularNewsViewModel>? LatestArticles { get; set; }



    }        
}
