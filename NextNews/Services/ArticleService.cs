﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NextNews.Data;
using NextNews.Models;
using NextNews.Models.Database;
using SQLitePCL;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Collections.Generic;
using Azure.Storage.Blobs;
using NextNews.ViewModels;




namespace NextNews.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        private readonly BlobServiceClient _blobServiceClient;

        public ArticleService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

          
            _blobServiceClient = new BlobServiceClient(_configuration["AzureWebJobsStorage"]);
        }


        public List<Article> GetArticles()
        {
            var objList = _context.Articles.Include(x => x.UsersLiked).ToList();
            return objList;
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



        public void IncreamentViews(ArticleDetailsViewModel vm)
        {
            if (vm.Article.Views is null)
            {
                vm.Article.Views = 1;
            }
            else
            {
                vm.Article.Views++;

            }
            _context.SaveChanges();

        }








        public IEnumerable<Article> GetArticlesByCategory(int categoryId)
        {
            return _context.Articles.Where(a => a.CategoryId == categoryId).ToList();
        }

        public async Task<string> UploadImage(IFormFile file) 
        {
            BlobContainerClient containerClient = _blobServiceClient
            .GetBlobContainerClient("articleimage");
            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
            await using (var stream = file.OpenReadStream()) 
            { 
            blobClient.Upload(stream);
            }
                return blobClient.Uri.AbsoluteUri;
        }

        public void CheckExpiredSubs() 
        { 
        var expiredSubscription = _context.Subscriptions.Where(s=> s.Expired < DateTime.Now ).ToList();
            foreach (var item in expiredSubscription)  
            {
                item.IsActive = false;
                _context.Update(item);
            }
            _context.SaveChanges() ;
        }
     
    }
}

