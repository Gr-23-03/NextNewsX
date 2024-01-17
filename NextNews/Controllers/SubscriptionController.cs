using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextNews.Models.Database;
using NextNews.Services;
using System.Drawing.Printing;

namespace NextNews.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        public SubscriptionController(ISubscriptionService subscriptionService, IUserService userService, UserManager<User> userManager)
        {
            _subscriptionService = subscriptionService;
            _userService = userService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            return View();
        }
       
      
        //Create Subscription For User
        [Authorize]
        public IActionResult CreateUserSubscription() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUserSubscription(Subscription input) 
        {

            var userId = _userManager.GetUserId(User);
           
            var subscription = new Subscription
            {
                UserId = userId,
                SubscriptionTypeId =input.SubscriptionTypeId
            };
           string resultmessage= _subscriptionService.CreateSubscriptionForUser(userId, input.SubscriptionTypeId);
            ViewBag.Message = resultmessage;
            return View("Index","Home");
        }

        public async Task<IActionResult> ListSubscription()
        {
            var subscription = await _subscriptionService.GetSubscriptionsAsync();
            return View(subscription);
        }

        public async Task<IActionResult> Details(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View(subscription);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View(subscription);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price, Created, Expired, PaymentComplete, UserId")] Subscription subscription ) 
        {
            if (id != subscription.Id) 
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            { 
            await _subscriptionService.UpdateSubscriptionAsync(subscription);
                return RedirectToAction("ListSubscription");
            }
            return View(subscription);
        }
        public async Task<IActionResult> Delete(int id) 
        {
            var subscription = await _subscriptionService.GetSubscriptionByIdAsync(id);
            if (subscription == null) 
            {
                return NotFound();
            }
            return View(subscription);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        { 
         await _subscriptionService.DeleteSubscriptionAsync(id);
            return RedirectToAction("ListSubscription");
        }

        //Create Subscriptions Types
        public async Task<IActionResult> CreateTypes() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTypes(SubscriptionType subscriptionType) 
        {
            if (ModelState.IsValid)
            {
               await _subscriptionService.CreateSubscriptionTypesAsync(subscriptionType);
                return RedirectToAction("Index");
            }
            else
                return View ("CreateTypes","subscriptionType");
        }
        //Details of Subscription types
        public async Task<IActionResult> TypesDetails(int id) 
        { 
        var subscriptionType= await _subscriptionService.GetSubscriptionTypeByIdAsync(id);
            return View(subscriptionType);
        }

        //List of Types of Subscription
        public async Task<IActionResult> SubscriptionTypeList() 
        {
            var subscriptionType = await _subscriptionService.GetSubscriptionTypesAsync();
            return View(subscriptionType);
        }

        //Edit Subscription Types
        public async Task<IActionResult> TypesEdit(int id) 
        {
        
            var subscriptionType = await _subscriptionService.GetSubscriptionTypeByIdAsync(id);
            if (subscriptionType == null) 
            {
                return NotFound();
            }
            
            return View(subscriptionType);
        }
        [HttpPost]
        public async Task<IActionResult> TypesEdit(int id,[Bind("Id, Name, Description, Price")] SubscriptionType subscriptionType) 
        {
            if (subscriptionType.Id != id) 
            {
                return NotFound();
            }

            if (ModelState.IsValid) 
            { 
            await _subscriptionService.UpdateSubscriptionTypeAsync(subscriptionType);
                return RedirectToAction("SubscriptionTypeList");
            }
            return View(subscriptionType);
        }

        // Delete Subscription Type
        public async Task<IActionResult> TypesDelete(int id) 
        {
            var subscriptionType = await _subscriptionService.GetSubscriptionTypeByIdAsync(id);
            if (subscriptionType == null) 
            {
                return NotFound();
            }
            return View(subscriptionType);
        }
        [HttpPost, ActionName("TypesDelete")]
        public async Task<IActionResult> TypesDeleteConfirm(int id) 
        { 
        await _subscriptionService.DeleteSubscriptionType(id);
            return RedirectToAction("SubscriptionTypeList");
        }

        public async Task<IActionResult> Bankcard() 
        {
            
            return View();
        }
    }
}
