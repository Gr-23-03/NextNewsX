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
        private readonly IStockService _stockService;
        private readonly ICategoryService _categoryService;
        
        public HomeController(ILogger<HomeController> logger ,IUserService userService, 
            IArticleService articleService, ICategoryService categoryService, IStockService stockService)
        {
            _logger = logger;
            _userService = userService;
            _articleService = articleService;
            _stockService = stockService;
            _categoryService = categoryService;
        }

        //public IActionResult Index()
        //{
        //    var users = _userService.GetUsers();
        //    _logger.LogInformation("Hello!");
        //    return View();
        //}



        /* Most popular articles
           Latest articles 
           Articles by categories */
        public IActionResult Index()
        {
            List<Article> allArticles = _articleService.GetArticles().ToList();
            List<Category> allCategories = _categoryService.GetCategories().ToList();

            int swedenId = allCategories.Where(a => a.Name == "Sweden").FirstOrDefault().Id;
            int localId = allCategories.Where(a => a.Name == "Local").FirstOrDefault().Id;
            int businessId = allCategories.Where(a => a.Name == "Business").FirstOrDefault().Id;
            int sportId = allCategories.Where(a => a.Name == "Sport").FirstOrDefault().Id;
            int worldId = allCategories.Where(a => a.Name == "World").FirstOrDefault().Id;
            int healthId = allCategories.Where(a => a.Name == "Health").FirstOrDefault().Id;
            int artAndCultureId = allCategories.Where(a => a.Name == "Art & Culture").FirstOrDefault().Id;
            int weatherId = allCategories.Where(a => a.Name == "Weather").FirstOrDefault().Id;
            int entertainmentId = allCategories.Where(a => a.Name == "Entertainment").FirstOrDefault().Id;

            var vm = new HomeIndexVM()
            {
                TopStoryArticle = allArticles.OrderByDescending(a => a.Views).FirstOrDefault(),
                TopStoryArticles = allArticles.OrderByDescending(a => a.Views).Take(5).ToList(),
                MostPopularArticle = allArticles.OrderByDescending(a => a.Likes).FirstOrDefault(),
                MostPopularArticles = allArticles.OrderByDescending(a => a.Likes).Take(5).ToList(),
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
                ArticlesByCategorySweden = allArticles.Where(a => a.CategoryId == swedenId).OrderByDescending(a => a.DateStamp).Take(3).ToList(),
                ArticlesByCategoryLocal = allArticles.Where(a => a.CategoryId == localId).OrderByDescending(a => a.DateStamp).Take(3).ToList(),
                ArticlesByCategoryWorld = allArticles.Where(a => a.CategoryId == worldId).OrderByDescending(a => a.DateStamp).Take(3).ToList(),
                ArticlesByCategoryBusiness = allArticles.Where(a => a.CategoryId == businessId).OrderByDescending(a => a.DateStamp).Take(4).ToList(),
                ArticlesByCategorySport = allArticles.Where(a => a.CategoryId == sportId).OrderByDescending(a => a.DateStamp).Take(4).ToList(),
                ArticlesByCategoryHealth = allArticles.Where(a => a.CategoryId == healthId).OrderByDescending(a => a.DateStamp).Take(4).ToList(),
                ArticlesByCategoryWeather = allArticles.Where(a => a.CategoryId == weatherId).OrderByDescending(a => a.DateStamp).Take(4).ToList(),
                ArticlesByCategoryArtAndCulture = allArticles.Where(a => a.CategoryId == artAndCultureId).OrderByDescending(a => a.DateStamp).Take(4).ToList(),
                ArticlesByCategoryEntertainment = allArticles.Where(a => a.CategoryId == entertainmentId).OrderByDescending(a => a.DateStamp).Take(4).ToList(),
                EditorsChoiceArticles = allArticles.Where(a => a.IsEditorsChoice == true).OrderByDescending(a => a.DateStamp).Take(4).ToList(),
            
            };

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

        //public async Task<IActionResult> StockReport()
        //{
        //    var stockReport = await _stockService.GetStockHttpClient("us");
        //    return View(stockReport);
        //}
        public ActionResult AboutUs()
        {
            return View();

        }
     
    
    }
}
