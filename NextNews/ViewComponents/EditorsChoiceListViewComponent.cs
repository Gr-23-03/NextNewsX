//using Microsoft.AspNetCore.Mvc;
//using NextNews.Models.Database;
//using NextNews.Services;

//namespace NextNews.ViewComponents
//{
//    public class EditorsChoiceListViewComponent : ViewComponent
//    {
//        private readonly IArticleService _articleService;

//        public EditorsChoiceListViewComponent(IArticleService articleService)
//        {
//            _articleService = articleService;
//        }

//        public IViewComponentResult Invoke()
//        {
//            var editorChoices = _articleService.GetEditorsChoiceArticles();
//            return View(editorChoices);
//        }
//    }
//}


// EditorsChoiceViewComponent.cs

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NextNews.Models.Database;

using NextNews.Services;


public class EditorsChoiceViewComponent : ViewComponent
{

    private readonly ICategoryService _categoryService;
    private readonly IArticleService _articleService;



    public EditorsChoiceViewComponent(ICategoryService categoryService, IArticleService articleService)
       {
         _categoryService = categoryService;           
         _articleService = articleService;

       }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Fetch your editor's choice articles from your data source
        // For example, you might use a service to retrieve the articles
        var editorChoiceArticles = _articleService.GetEditorsChoiceArticles();

        return View(editorChoiceArticles.ToList());
    }

}
