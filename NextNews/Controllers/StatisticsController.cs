using Microsoft.AspNetCore.Mvc;
using NextNews.Services;
using NextNews.ViewModels;

namespace NextNews.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        public StatisticsController(IArticleService articleService , IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }

        public IActionResult Index()
        {

            // Fetching user count and article count from services
            int userCount = _userService.GetUsers().Count();
            int articleCount = _articleService.GetArticles().Count();

            // Creating a view model to pass the counts to the view
            var viewModel = new StatisticsViewModel
            {
                UserCount = userCount,
                ArticleCount = articleCount
            };

            return View(viewModel);
        }

        // Other actions related to statistics can be added here
    }

}
    

