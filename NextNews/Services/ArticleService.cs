using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NextNews.Data;
using NextNews.Models;
using NextNews.Models.Database;
using SQLitePCL;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Collections.Generic;



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
            return _context.Articles.Include(x => x.UsersLiked).ToList();
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

   
        //Add no. of likes
        public void AddLikes(int articleId, string userId)
        {
            var article = _context.Articles.Include(x => x.UsersLiked).FirstOrDefault(x => x.Id == articleId);

            if (article is null) return;

            if (article.UsersLiked.Any(x => x.Id == userId))
            {
                article.UsersLiked = article.UsersLiked.Where(x => x.Id != userId).ToList();
            }
            else
            {
                var user = _context.Users.Find(userId);
                if (user is null) return;

                article.UsersLiked.Add(user);
            }

            _context.SaveChanges();

        }

        
        public void IncreamentViews(Article article)
        {
            if (article.Views is null)
            {
                article.Views = 1;
            }
            else
            {
                article.Views++;
            }
            _context.SaveChanges();

        }
        public IEnumerable<Article> GetArticlesByCategory(int categoryId)
        {
            return _context.Articles.Where(a => a.CategoryId == categoryId).ToList();
        }


    }
}

