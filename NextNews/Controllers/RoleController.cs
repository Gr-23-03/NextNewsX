using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NextNews.Models.Database;
using NextNews.Services;

namespace NextNews.Controllers
{

    //// For Admin-specific actions
    //[Authorize(Policy = "Admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager )
        {
            _roleManager = roleManager;
           
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }


        public IActionResult ListOfRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }



       








    }
}
