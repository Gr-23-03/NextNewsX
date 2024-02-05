using Microsoft.AspNetCore.Mvc;
using NextNews.Models.Database;
using NextNews.Services;

namespace NextNews.Controllers
{
    public class NewsLetterController : Controller
    {
        private readonly INewsLetterService _newsLetterService;
        public NewsLetterController(INewsLetterService newsLetterService) 
        { 
        _newsLetterService = newsLetterService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(NewsLetterSubscriber newsLetterSubscriber) 
        { 
        _newsLetterService.CreateNLSubscriber(newsLetterSubscriber);
            return RedirectToAction("Index","Home"); ;
        }
    }
}
