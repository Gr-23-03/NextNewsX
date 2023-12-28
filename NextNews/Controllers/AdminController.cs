using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextNews.Models.Database;
using NextNews.Services;

namespace NextNews.Controllers
{
    public class AdminController : Controller
    {

        private readonly IRoleManagementService _roleManagementService;
        private readonly IArticleService _articleService;

        public AdminController(IRoleManagementService roleManagementService, IArticleService articleService)
        {
            _roleManagementService = roleManagementService;
            _articleService = articleService;
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


        [Authorize(Roles = "Editor")]
        public IActionResult ManageArticles()
        {
            // getting a list of articles from the service
            var articles = _articleService.GetArticles();

            
            return View(articles);
        }

        [Authorize(Roles = "Editor")]
        public IActionResult EditArticle(int id)
        {
            // getting the article by ID from the service
            var article = _articleService.GetArticleByIdAsync(id).Result;

           
            return View(article);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditArticle(Article article)
        {
            // Updating the article using the service
            await _articleService.UpdateArticleAsync(article);

            //  redirecting to the article  page
            return RedirectToAction("ManageArticles");
        }

        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            // Deleting the article using the service
            await _articleService.DeleteArticleAsync(id);
            
            return RedirectToAction("ManageArticles");
        }


    }
}
