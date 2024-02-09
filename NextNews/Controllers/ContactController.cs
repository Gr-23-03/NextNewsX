using Microsoft.AspNetCore.Mvc;
using NextNews.Models.Database;
using NextNews.Services;

namespace NextNews.Controllers
{
    
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
                _contactService = contactService;
        }






        [HttpGet]
        public ActionResult Index()
        {
            

            return View();
        }


        [HttpPost]
        public ActionResult SaveContactFormMessage(ContactFormMessage obj)
        {
            if (ModelState.IsValid)
            {
                // Process the form data (e.g., send an email, save to a database).
                _contactService.SaveContactMessage(obj);

                // For this example, let's just return a view with the submitted data.
                return View("ThankYouMessage");
            }
            else
            {
                // If the model state is not valid, return the form with errors.
                ViewBag.ErrorMessage = "Have all necesary fields information added?";
                return View("SaveContactFormMessage", obj);
            }
        }

    }
}