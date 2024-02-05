using NextNews.Models;
using NextNews.Models.Database;
using NextNews.ViewModels;
using System.Collections.Generic;

namespace NextNews.Services
{
    public interface IArticleService
    {
        List<Article> GetArticles();

        public void AddArticle(Article article);
        public Task<Article> GetArticleByIdAsync(int id);
        public Task UpdateArticleAsync(Article article);
        public Task DeleteArticleAsync(int id);
        public List<Category> GetCategories();

        public void AddLikes(int id, string userId );
        public void IncreamentViews(ArticleDetailsViewModel article);
        IEnumerable<Article> GetArticlesByCategory(int categoryId);
        public Task<string> UploadImage(IFormFile file);
        public void CheckExpiredSubs();
        public Task<List<LatestNewsViewModel>> LatestArticles();

    }
}
