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

        public HomeController(ILogger<HomeController> logger ,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        public IActionResult Index()
        {

            var users = _userService.GetUsers();
            _logger.LogInformation("Hello");

            return View(); 


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
