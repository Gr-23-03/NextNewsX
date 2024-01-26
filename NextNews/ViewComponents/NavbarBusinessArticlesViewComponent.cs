
using Microsoft.AspNetCore.Mvc;
using NextNews.Services;

namespace NextNews.ViewComponents
{
    public class NavbarBusinessArticlesViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public NavbarBusinessArticlesViewComponent(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }



        public IViewComponentResult Invoke()
        {

            var categoryId = _categoryService.GetCategories().Where(c => c.Name == "Business").FirstOrDefault().Id;

            var objList = _articleService.GetArticles().Where(a => a.CategoryId == categoryId).OrderByDescending(a => a.DateStamp).Take(4).ToList();

            return View(objList);
        }


    }
}
