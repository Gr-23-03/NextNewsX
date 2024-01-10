using Microsoft.AspNetCore.Authorization;
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


        [Authorize(Roles = "Admin")]
        public IActionResult ManageUsers()
        {
            // Getting a list of users from the service
            var users = _userService.GetUsers();

            return View(users);
        }


/*

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            try
            {
                var editUser = await _userService.GetUserByIdAsync(id);

                if (editUser == null)
                {
                    return NotFound();
                }

                return View("editUser", editUser);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //public async Task<IActionResult> EditUser(User user)
        //{
        //    try
        //    {
        //        user.NormalizedEmail = user.Email.ToUpper();
        //        user.UserName = user.Email.ToUpper();
        //        user.NormalizedUserName = user.UserName.ToUpper();


        //        if (ModelState.IsValid)
        //        {
        //            await _userService.UpdateUserAsync(user);
        //            return RedirectToAction("ManageUsers");
        //        }
        //        else
        //        {
        //            return View("edituser", user);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return View("Error");
        //    }
        //}



        [HttpPost]
      
        [Authorize(Roles = "Admin")] // Only authorized users can edit categories
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,DateofBirth,PhoneNumber,Email")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _userService.UpdateUserAsync(user);
                return RedirectToAction(nameof(ManageUsers));
            }

            return View(user);
        }
        */




        */




        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(ManageUsers));
           
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed()
        {
            
            return View();
        }




    }
}
