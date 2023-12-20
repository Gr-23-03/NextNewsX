using NextNews.Models.Database;

namespace NextNews.Services
{
    public interface IArticleService
    {
        List<Article> GetArticles();

        public void AddArticle(Article article);

    }
}
