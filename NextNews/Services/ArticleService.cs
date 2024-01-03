using NextNews.Data;
using NextNews.Models;
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

        //details category
        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }


        //Update category
        public async Task UpdateArticleAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }


        //delete category

        public async Task DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }


        //// Retrieve the latest news as LatestNewsViewModel instances
        //public async Task<IEnumerable<LatestNewsViewModel>> GetLatestNewsViewModels()
        //{
        //    // Retrieve the latest published articles
        //    var latestPublishedArticles = _context.Articles
        //        .OrderByDescending(article => article.DateStamp)
        //        .Take(5)
        //        .ToList();

        //    // Convert articles to view models
        //    var latestNewsViewModels = latestPublishedArticles
        //        .Select(article => new LatestNewsViewModel
        //        {
        //            HeadLine = article.HeadLine,
        //            ContentSummary = article.ContentSummary,
        //            DateStamp = (DateTime)article.DateStamp
        //        });

        //    return latestNewsViewModels;
        //}



    }
}

