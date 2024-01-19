using Microsoft.AspNetCore.Mvc;
using NextNews.Models;
using NextNews.Models.Database;
using NextNews.Services;
using NextNews.ViewModels;
using System.Diagnostics;

namespace NextNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IArticleService _articleService;

        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger ,IUserService userService, 
            IArticleService articleService, ICategoryService categoryService)
        {
            _logger = logger;
            _userService = userService;
            _articleService = articleService;

            _categoryService = categoryService;
        }

        //public IActionResult Index()
        //{
        //    var users = _userService.GetUsers();
        //    _logger.LogInformation("Hello!");
        //    return View();
        //}



        /* Most popular articles
           Latest articles */
        public IActionResult Index()
        {
            List<Article> allArticles = _articleService.GetArticles().ToList();
            List<Category> allCategories = _categoryService.GetCategories().ToList();

            int sportId = allCategories.Where(a => a.Name == "Sport").FirstOrDefault().Id;
            int worldId = allCategories.Where(a => a.Name == "Sport").FirstOrDefault().Id;


            var vm = new HomeIndexVM()
            {
                PopularArticle = allArticles.OrderByDescending(a => a.Views).FirstOrDefault(),
                PopularArticles = allArticles.OrderByDescending(a => a.Views).Take(3).ToList(),
                LatestArticle = allArticles.OrderByDescending(obj => obj.DateStamp).FirstOrDefault(),

                LatestArticles = allArticles
                    .OrderByDescending(obj => obj.DateStamp)
                    .Take(5)
                    .Select(obj => new LatestNewsViewModel()
                    {
                        Id = obj.Id,
                        HeadLine = obj.HeadLine,
                        DateStamp = obj.DateStamp,
                        ContentSummary = obj.ContentSummary,
                        ImageLink = obj.ImageLink,
                    }).ToList(),
                AllCategories = allCategories,
                ArticlesByCategorySport = allArticles.Where(a => a.CategoryId == sportId).OrderByDescending(a => a.DateStamp).Take(5).ToList(),
                ArticlesByCategoryWorld = allArticles.Where(a => a.CategoryId == worldId).OrderByDescending(a => a.DateStamp).Take(5).ToList(),
            };





            //// Set a specific article by its ID
            //var specificArticleId = 5 /* specify the desired article ID */;
            //vm.SpecificArticle = allArticles.FirstOrDefault(article => article.Id == specificArticleId);

            // Set the specific articles by their IDs
            var specificArticleIds = new List<int> { 11, 17, 25, 13, 15, 7, 12, 8, 16, 22, 18, 6, 24, 34 }; // Example IDs
            vm.SpecificArticles = allArticles.Where(article => specificArticleIds.Contains(article.Id)).ToList();




            return View(vm); 
        }


        public IActionResult Privacy()
        {
            return View();
        }


         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
         public IActionResult Error()
         {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
         }



    }
}
