using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextNews.Models.Database;
using NextNews.Services;

namespace NextNews.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService) 
        { 
            _articleService = articleService;
        }



        public IActionResult Index()
        {
            return RedirectToAction(nameof(ListArticles));
        }

        //Action for list of article
        public IActionResult ListArticles() 
        { 
        var articles= _articleService.GetArticles();
            return View(articles);
        }

        //Action to Add article
        //[HttpPost]
        public IActionResult AddArticle( Article article) 
        {
            if (ModelState.IsValid) 
            { 
            _articleService.AddArticle(article);
                return RedirectToAction("ListArticles");
            }
        return  View("CreateArticle", article);
        }

        public IActionResult CreateArticle()
        {
            return View();
        }

        //details
        public async Task<IActionResult> Details(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }


        [Authorize]
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
        [Authorize] // Only authorized users can edit categories
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

        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleService.DeleteArticleAsync(id);
            return RedirectToAction(nameof(ListArticles));
        }


    }
}
