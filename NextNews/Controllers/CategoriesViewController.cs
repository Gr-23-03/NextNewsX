using Microsoft.AspNetCore.Mvc;
using NextNews.Models.Database;
using NextNews.Services;

namespace NextNews.Controllers
{
    public class CategoriesViewController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public CategoriesViewController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService; 
        }

        public IActionResult Index()
        {
            var categories = _articleService.GetCategories();
            return View(categories);
        }


        public IActionResult ArticlesByCategories(int categoryId)
        {
            var articles = _articleService.GetArticlesByCategory(categoryId);

            @ViewBag.CategoryName = _categoryService.GetCategoryById(categoryId).Name;

            return View(articles);
        }

    }
}
