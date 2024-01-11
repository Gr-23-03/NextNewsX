using Microsoft.AspNetCore.Mvc;
using NextNews.Models;
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

        public HomeController(ILogger<HomeController> logger ,IUserService userService, 
            IArticleService articleService)
        {
            _logger = logger;
            _userService = userService;
            _articleService = articleService;
        }
       
        public IActionResult Index()
        {
            var vm = new HomeIndexVM()
            {

                PopularArticles = _articleService.GetArticles()
                     .OrderByDescending(a => a.Views)
                     .Take(5)
                     .ToList(),
                LatestArticles = _articleService.GetArticles()
                    .OrderByDescending(obj => obj.DateStamp)
                    .Take(5)
                    .Select(obj => new PopularNewsViewModel()
                    {
                        Id = obj.Id,
                        HeadLine = obj.HeadLine,
                        DateStamp = obj.DateStamp,
                        ContentSummary = obj.ContentSummary,
                        ImageLink = obj.ImageLink,
                    }).ToList()
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



    }
}
