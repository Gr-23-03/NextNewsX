﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NextNews.Data;
using NextNews.Models;
using NextNews.Models.Database;
using NextNews.Services;
using NextNews.ViewModels;

namespace NextNews.Controllers
{
    public class CategoriesController : Controller
    {
        

        private readonly ICategoryService _categoryService;


        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _context = context;
        }



        //display list of categories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return View(categories);
        }


        //create

        [Authorize(Roles = "Editor")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] 
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
                
            }

            return View(category);
        }


        //details
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Only authorized users can edit categories
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [Authorize] 
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }




        // search articles by category and article headline and by words

        public async Task<IActionResult> Search(string searchString, int pg = 1)
        {
            const int pageSize = 9;
            if (pg < 1)
                pg = 1;

            var articlesQuery = _context.Articles.Include(a => a.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToLower();
                articlesQuery = articlesQuery.Where(article => (article.Category != null && article.Category.Name.ToLower().Contains(searchString)) ||
                                                               article.HeadLine.ToLower().Contains(searchString) ||
                                                               article.ContentSummary.ToLower().Contains(searchString) ||
                                                               article.Content.ToLower().Contains(searchString));
            }

            // Counting the total records that match the search criteria
            int recsCount = await articlesQuery.CountAsync();

            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            // Fetching the paginated articles
            var paginatedArticles = await articlesQuery.Skip(recSkip).Take(pageSize).ToListAsync();

            var categoryQuery = from c in _context.Categories
                                orderby c.Id
                                select c.Name.ToLower();

            var viewModel = new CategoryViewModel
            {
                CategoryNames = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Articles = paginatedArticles, // Using the paginated list of articles
                SearchString = searchString, // Passing the search string back to the view
                Pager = pager // Adding pager to the viewModel
            };

            return View(viewModel);
        }


      



    }
}










