using Microsoft.AspNetCore.Mvc.Rendering;
using NextNews.Models;
using NextNews.Models.Database;

namespace NextNews.ViewModels
{
    public class CategoryViewModel
    {

        public SelectList CategoryNames { get; set; }  // SelectList for category names for search bar
        public List<Article> Articles { get; set; }  // List of articles to display in the view
        public string SelectedCategory { get; set; }
        public string SearchString { get; set; } 
        public Pager Pager { get; set; }


    }
}
