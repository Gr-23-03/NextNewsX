using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace NextNews.Models.Database;

public class User: IdentityUser
{
    [Required]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime DateofBirth { get; set; }


    public ICollection<Subscription>? Subscriptions { get; set; }

    
}
