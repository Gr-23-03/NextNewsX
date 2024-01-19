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




        public List<Category>? AllCategories { get; set; }

        public List<Article>? ArticlesByCategorySport { get; set; } // Added property for category-specific articles

        public List<Article>? ArticlesByCategoryWorld { get; set; } // Added property for category-specific articles

        public Article SpecificArticle { get; set; }

        public List<Article>? SpecificArticles { get; set; }

    }
}
