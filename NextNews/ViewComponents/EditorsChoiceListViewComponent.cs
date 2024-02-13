﻿using Microsoft.AspNetCore.Mvc;
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
        var editorChoiceArticles = _articleService.GetEditorsChoiceArticles();

        return View(editorChoiceArticles.ToList());
    }

}
