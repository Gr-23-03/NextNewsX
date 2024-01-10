using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextNews.Models;
using NextNews.Models.Database;
using NextNews.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NextNews.ViewModels;

namespace NextNews.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;


        public ArticleController(IArticleService articleService, IUserService userService, UserManager<User> userManager)
        {
            _articleService = articleService;
            _userService = userService;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return RedirectToAction(nameof(ListArticles));
        }


        // Latest articles
        public ActionResult LatestArticles()
        {
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


        //Action for list of article

        public IActionResult ListArticles()
        {
            var articles = _articleService.GetArticles();

            return View(articles);
        }

        //Action to Add article

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult AddArticle(Article article)
        {



            if (ModelState.IsValid)
            {
                _articleService.AddArticle(article);

                return RedirectToAction("ListArticles");
            }

            // If there is an error, repopulate the categories
            var categories = _articleService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View("CreateArticle", article);
        }


        [Authorize(Roles = "Editor")]
        public IActionResult CreateArticle()
        {
            var categories = _articleService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }


        //details

        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Details(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
           
            if (article == null)
            {
                return NotFound();
            }

            _articleService.IncreamentViews(article);

            //// Check if the current user has liked the article
            //var userId = _userManager.GetUserId(User);
            //var userHasLiked = article.UsersLiked.Any(u => u.Id == userId);

            //// Create a view model
            //var viewModel = new ArticleDetailsViewModel
            //{
            //    Article = article,
            //    IsLikedByUser = userHasLiked
            //};
            return View(article);
        }


        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")] // Only authorized users can edit categories
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateStamp,LinkText,HeadLine,ContentSummary,Content,Views,Likes, ImageLink,AuthorName,CategoryId")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _articleService.UpdateArticleAsync(article);
                return RedirectToAction(nameof(ListArticles));
            }

            return View(article);
        }

        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleService.DeleteArticleAsync(id);
            return RedirectToAction(nameof(ListArticles));
        }

        // Method to add likes
        [Authorize]
        
        public IActionResult Likes(int id, string returnUrl)
        {
            var userId = _userManager.GetUserId(User)!;
            _articleService.AddLikes(id, userId);

            return Json(new { success = true, message = "Action performed successfully" });

        }

       
       
    }
}
