using Microsoft.AspNetCore.Mvc;
using NextNews.Models.Database;
using NextNews.Services;

namespace NextNews.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(string username)
        {
            List<User> users = await _userService.GetUsersAsync();

            //if (!String.IsNullOrEmpty(firstname))          // use for firstname
            //{
            //    users = users.Where(x => x.FirstName.Contains(firstname)).ToList();
            //}


            if (!String.IsNullOrEmpty(username))
            {
                users = users.Where(x => x.UserName.Contains(username)).ToList();
            }

            return View(users);
        }
    }
}
