using System.ComponentModel.DataAnnotations;

namespace NextNews.ViewModels
{
    public class AdminUserVM
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; internal set; }
        public List<RolesVM> Roles { get; set; }
    }
}
