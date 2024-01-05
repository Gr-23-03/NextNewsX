using Microsoft.AspNetCore.Mvc;

namespace NextNews.Controllers
{
    public class SubscriptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
