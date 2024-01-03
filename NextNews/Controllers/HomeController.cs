using Microsoft.AspNetCore.Mvc;
using NextNews.Models;
using NextNews.Services;
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

            var users = _userService.GetUsers();
            _logger.LogInformation("Hello");

            var latestArticles = _articleService.GetArticles().OrderByDescending(obj => obj.DateStamp).Take(5).ToList();

            List<LatestNewsViewModel> vmList = new List<LatestNewsViewModel>();

            foreach (var item in latestArticles)
            {
                var vm = new LatestNewsViewModel()
                {
                    Id = item.Id,
                    HeadLine = item.HeadLine,
                    DateStamp = item.DateStamp,
                    ContentSummary = item.ContentSummary
                };

                vmList.Add(vm);
            }

            return View(vmList);

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
