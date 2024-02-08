﻿using NextNews.Models;
using NextNews.Models.Database;

namespace NextNews.ViewModels
{
    public class HomeIndexVM
    {

        public List<Article>? TopStoryArticles { get; set; }
        public Article? TopStoryArticle { get; set; }

        public List<Article>? MostPopularArticles { get; set; }
        public Article? MostPopularArticle { get; set; }

        //public List<Article>? LatestArticles { get; set; }
        public Article? LatestArticle { get; set; }
        
        //public List<LatestNewsViewModel>? LatestArticle { get; set; }
        public List<LatestNewsViewModel>? LatestArticles { get; set; }


        public List<Category>? AllCategories { get; set; }
        public List<Article>? ArticlesByCategorySweden { get; set; }
        public List<Article>? ArticlesByCategoryLocal { get; set; }
        public List<Article>? ArticlesByCategoryBusiness { get; set; }
        public List<Article>? ArticlesByCategorySport { get; set; } // Added property for category-specific articles
        public List<Article>? ArticlesByCategoryWorld { get; set; } 
        public List<Article>? ArticlesByCategoryHealth { get; set; }
        public List<Article>? ArticlesByCategoryWeather { get; set; }
        public List<Article>? ArticlesByCategoryArtAndCulture { get; set; }
        public List<Article>? ArticlesByCategoryEntertainment { get; set; }



        //public Article? SpecificArticle { get; set; }
        //public List<Article>? SpecificArticles { get; set; }


        public List<Article>? EditorsChoiceArticles { get; set; }
        //public List<Article>? EditorsChoiceArticlesA { get; set; }
        //public List<Article>? EditorsChoiceArticlesB { get; set; }
        //public List<Article>? EditorsChoiceArticlesC { get; set; }
        //public List<Article>? EditorsChoiceArticlesD { get; set; }

    }
}
