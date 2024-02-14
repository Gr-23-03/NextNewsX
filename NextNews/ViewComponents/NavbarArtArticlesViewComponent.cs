﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextNews.Services;
using NextNews.ViewModels;

namespace NextNews.ViewComponents
{
    public class NavbarArtArticlesViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public NavbarArtArticlesViewComponent (IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Premium")]
        public IViewComponentResult Invoke()
        {

            var categoryId = _categoryService.GetCategories().Where(c => c.Name == "Art & Culture").FirstOrDefault().Id;

            var allArticles = _articleService.GetArticles();
            var objListInBoxes = allArticles.Where(a => a.CategoryId == categoryId).OrderByDescending(a => a.DateStamp).Take(4).ToList();
            var objListInList = allArticles.Where(a => a.CategoryId == categoryId).OrderByDescending(a => a.DateStamp).Take(10).ToList();

            NavbarCategoryVM vm = new NavbarCategoryVM()
            {
                ArticlesInBoxes = objListInBoxes,
                ArticlesInList = objListInList,
                CategoryName = "Art & Culture",
            };



            return View(vm);
        }


    }
}
