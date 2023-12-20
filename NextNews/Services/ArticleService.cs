using NextNews.Data;
using NextNews.Models.Database;

namespace NextNews.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;


        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Article> GetArticles()
        {
            
            return _context.Articles.ToList();
        }
        public void AddArticle(Article article) 
        { 
            _context.Articles.Add(article);
            _context.SaveChanges();
        }
    }
}
