using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextNews.Models.Database;
using NextNews.Services;
using NextNews.ViewModels;

namespace NextNews.Controllers
{
    public class AdminController : Controller
    {

        private readonly IRoleManagementService _roleManagementService;
        private readonly IArticleService _articleService;
        private object _context;
        private readonly IUserService _userService;

        public AdminController(IRoleManagementService roleManagementService,IArticleService articleService ,IUserService userService)
        {
            _roleManagementService = roleManagementService;
            _articleService = articleService;
            _userService = userService;
        }




        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            // Fetching user count and article count from services
            int userCount = _userService.GetUsers().Count();
            int articleCount = _articleService.GetArticles().Count();

            // Pass counts directly to the view
            ViewData["UserCount"] = userCount;
            ViewData["ArticleCount"] = articleCount;

            return View();
        }





        // This is for creating dynamic role wihout seeding

        /*
            [HttpGet]
            public IActionResult CreateRole()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> CreateRole(string roleName)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                bool result = await _roleManagementService.CreateRoleAsync(roleName);

                if (result)
                {

                    return RedirectToAction("CreationSuccess", "Admin");
                }
                else
                {

                    return View("CreationFailed", "Admin");
                }
            }


            [HttpGet]
            public IActionResult CreationSuccess()
            {        
                return View();
            }


            [HttpGet]
            public IActionResult CreationFailed()
            {
                return View();
            }

    */

        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }


    }
}
