using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NextNews.Data;
using NextNews.Models.Database;
using NextNews.Services;
using NextNews.ViewModels;
using NextNews.Views.Shared.Components.SearchBar;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<IActionResult> Search(string searchString, int pg = 1, int perPage = 10)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                searchString = "";    // If searchSting is null then we create a empty string
            }
            else
            {
                searchString = searchString.Trim().ToLower();   //  Take away space on the beginning and end of searchString.
            }



            var categoryQuery = from c in _context.Categories 
                                orderby c.Id
                                select c.Name.ToLower();
            var articles = _context.Articles
            .Where(a => a.HeadLine.Contains(searchString) || a.Content.Contains(searchString))
                .ToList();


            int totalCount = _context.Articles.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / perPage);
            var pagginatedArticles = articles               
                .Skip((pg - 1) * perPage)
                .Take(perPage)
                .ToList();


            ViewBag.CurrentPage = pg;
            ViewBag.TotalPages = totalPages;



            SPager pagginationObj = new SPager(articles.Count, pg, perPage) 
            { 
                Action = "Search", 
                Controller = "Categories", 
                SearchText = searchString
            };   



            var viewModel = new CategoryViewModel
            {
                CategoryNames = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Articles = pagginatedArticles,
                SearchString = searchString, // Passing the search string back to the view
                Paggination = pagginationObj
            };

    
            return View(viewModel);
        }



    }
}










