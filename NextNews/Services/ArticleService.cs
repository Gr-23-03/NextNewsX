using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<Category> categories = _context.Categories.ToList();

        }

        //details
        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }


        //Update 
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

        //Get Categories to select them in create view
        public List<Category> GetCategories() 
        { 
            return _context.Categories.ToList();
        }

        
    }
}

