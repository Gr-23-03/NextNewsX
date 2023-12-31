using NextNews.Models.Database;

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

    }
}
