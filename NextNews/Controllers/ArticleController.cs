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
            return View();
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

    }
}
