using System.ComponentModel.DataAnnotations;

namespace NextNews.ViewModels
{
    public class RolesVM
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public bool Ischecked { get; set; }
    }
}
