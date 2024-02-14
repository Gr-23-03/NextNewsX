﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextNews.Services;
using NextNews.ViewModels;

namespace NextNews.ViewComponents
{
    public class NavbarHealthArticlesViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public NavbarHealthArticlesViewComponent(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }


        [Authorize(Roles = "Premium")]
        public IViewComponentResult Invoke()
        {

            var categoryId = _categoryService.GetCategories().Where(c => c.Name == "Health").FirstOrDefault().Id;

            var allArticles = _articleService.GetArticles();
            var objListInBoxes = allArticles.Where(a => a.CategoryId == categoryId).OrderByDescending(a => a.DateStamp).Take(4).ToList();
            var objListInList = allArticles.Where(a => a.CategoryId == categoryId).OrderByDescending(a => a.DateStamp).Take(10).ToList();

            NavbarCategoryVM vm = new NavbarCategoryVM()
            {
                ArticlesInBoxes = objListInBoxes,
                ArticlesInList = objListInList,
                CategoryName = "Health",
            };



            return View(vm);
        }


    }
}


