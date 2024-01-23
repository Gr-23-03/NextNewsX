using NextNews.Models;
using NextNews.Models.Database;
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

        Task<string> UploadImage(IFormFile file);   //azure storage
        public void AddLikes(int id, string userId );
        public void IncreamentViews(Article article);
        IEnumerable<Article> GetArticlesByCategory(int categoryId);
    }
}
