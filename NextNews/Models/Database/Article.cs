using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextNews.Models.Database
{
    public class Article
    {
       [Key]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateStamp { get; set; }

        public string? LinkText { get; set; }
        public string? HeadLine { get; set; }
        
        public string? ContentSummary { get; set; }

        public string? Content { get; set; }

        public int? Views { get; set; }

        public int? Likes { get; set;}

        //public int? Dislike { get; set; }
        public string? ImageLink { get; set; }
        
        [Display(Name = "Author:")]
        public string? AuthorName { get; set; }

        public int CategoryId {  get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
