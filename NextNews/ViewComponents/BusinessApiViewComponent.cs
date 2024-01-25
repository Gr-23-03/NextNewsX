using Microsoft.AspNetCore.Mvc;
using NextNews.Services;

namespace NextNews.ViewComponents
{
    public class BusinessApiViewComponent : ViewComponent
    {
        private readonly IStockService _stockService;

        public BusinessApiViewComponent(IStockService stockService)
        {
            _stockService = stockService;       
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {


            var api = await _stockService.GetStockHttpClient("us");

            return View(api);
        }


    }
}
