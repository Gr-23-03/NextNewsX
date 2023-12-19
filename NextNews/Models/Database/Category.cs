using System.ComponentModel.DataAnnotations;

namespace NextNews.Models.Database
{
    public class Category
    {
       
        public int Id { get; set; }
                
        public string? Name { get; set; }

        public  ICollection<Article>? Articles {  get; set; }    

    }
}
