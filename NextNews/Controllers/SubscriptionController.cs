using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NextNews.Models.Database;
using NextNews.Services;
using Stripe.Checkout;
using Subscription = NextNews.Models.Database.Subscription;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Security.Claims;
using Azure.Core;
using NextNews.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe;
using NextNews.Data.Migrations;

namespace NextNews.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        public SubscriptionController(ISubscriptionService subscriptionService, IUserService userService, UserManager<User> userManager, IEmailSender emailSender)
        {
            _subscriptionService = subscriptionService;
            _userService = userService;
            _userManager = userManager;
            _emailSender = emailSender;
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
        public async Task<IActionResult> CreateUserSubscription(Subscription input)
        {
            var userId = _userManager.GetUserId(User);
            // Check if the user already has this subscription
            string resultMessage = _subscriptionService.CheckExistingSubscription(userId, input.SubscriptionTypeId);
            if (resultMessage != "Eligible for subscription")
            {
                ViewBag.Message = resultMessage;
                return View("Index", "Home");
            }
            // Redirect to Stripe for payment
            return await StripeCheckout(userId, input.SubscriptionTypeId);
        }
        private async Task<IActionResult> StripeCheckout(string userId, int subscriptionTypeId)
        {
            // Retrieve subscription type details
            
            var subscriptionType = _subscriptionService.GetSubscriptionType(subscriptionTypeId);
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            // Create Stripe Checkout session

            var domain = "https://localhost:44349/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(subscriptionType.Price * 100), // Price in cents
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = subscriptionType.Name,
                    },
                },
                Quantity = 1,
            },
        },
                Metadata = new Dictionary<string, string>
                {
                    {"UserId", userId},
                    {"SubscriptionTypeId", subscriptionTypeId.ToString()}
                },
                Mode = "payment",
                CustomerEmail = userEmail,
                SuccessUrl =domain+$"Subscription/CompleteSubscription",
                CancelUrl = "https://yourdomain.com/subscription/cancel"
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            //return Redirect(session.Url);
        }


        public IActionResult CompleteSubscription()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());

            if (session.PaymentStatus == "paid")
            {

                var userId = session.Metadata["UserId"];
                var subscriptionTypeId = int.Parse(session.Metadata["SubscriptionTypeId"]);
                _subscriptionService.CompleteSubscription(userId, subscriptionTypeId);

                string htmlTemplate = System.IO.File.ReadAllText("C:\\Users\\zain\\source\\repos\\NextNews\\NextNews\\Views\\Shared\\EmailTemplate.cshtml");
                string personalizedContent = $"Welcome to your NextNews account. You can use your account to sign into the NextNews website and use special features. Subscriptions to the suite of NextNews newsletters can also be managed using your account. The NextNews takes your data privacy seriously. To learn more, read our privacy policy and account FAQs.All the best, Regards NextNews";
                string htmlMessage = htmlTemplate.Replace("{{main_content}}", personalizedContent);
                _emailSender.SendEmailAsync(userEmail, "NextNews Subscription", htmlMessage);
                return View("Success");
            }
            return View("Register");


        }
        public IActionResult Success() 
        
        {
            
         return View();
        }

     

        public IActionResult ListSubscription()
        {
            var subscription =  _subscriptionService.GetSubscriptionsAsync();
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

        //here start changing subscription

       
        public async Task<IActionResult> ChangeSubscription(int newSubscriptionTypeId)
        {
            var userId = _userManager.GetUserId(User);
            _subscriptionService.UpgradeSubscription(userId, newSubscriptionTypeId);

            // Redirect to a confirmation page or display a success message
           
            return await StripeCheckout(userId, newSubscriptionTypeId);
        }

        public async Task<IActionResult> SubscriptionHistory() 
        {
            var userId = _userManager.GetUserId(User); // Assuming you are using ASP.NET Core Identity
            var subscriptions = await _subscriptionService.GetUserSubscriptionsAsync(userId);
            List<SubscriptionHistoryViewModel> vmList = new List<SubscriptionHistoryViewModel>();
            foreach (var item in subscriptions) 
            {
                var vm = new SubscriptionHistoryViewModel()
                {
                    SubscriptionId= item.SubscriptionTypeId,
                  
                    Created =item.Created,
                    Expired = item.Expired,
                    Price = item.Price,
                    IsActive = item.IsActive,

                };
                vmList.Add(vm);
            }
            return View(vmList);
        }



    }
}
