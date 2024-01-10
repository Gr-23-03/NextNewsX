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



        public IActionResult Business()
        {
            var articles = _articleService.GetArticlesByCategory(1); // Replace 1 with the actual category ID for Business
            return View(articles);
        }
        public IActionResult Sport()
        {
            var articles = _articleService.GetArticlesByCategory(2); 
            return View(articles);
        }
        public IActionResult World()
        {
            var articles = _articleService.GetArticlesByCategory(3);
            return View(articles);
        }
        public IActionResult Local()
        {
            var articles = _articleService.GetArticlesByCategory(4);
            return View(articles);
        }
        public IActionResult Sweden()
        {
            var articles = _articleService.GetArticlesByCategory(5);
            return View(articles);
        }
        public IActionResult Weather()
        {
            var articles = _articleService.GetArticlesByCategory(6);
            return View("Categories/Business", articles);
        }
        public IActionResult Entertainment()
        {
            var articles = _articleService.GetArticlesByCategory(7);
            return View("Categories/Business", articles);
        }
    }
}
