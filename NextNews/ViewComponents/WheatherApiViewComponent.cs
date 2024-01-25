﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using NextNews.Services;

namespace NextNews.ViewComponents
{
    public class WheatherApiViewComponent : ViewComponent
    {

        private readonly IWheatherService _weatherService;

        public WheatherApiViewComponent(IWheatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string chosenCity)
        {
            var weatherData = await _weatherService.GetWeatherReport("Linköping");
            return View(weatherData);
        }





    }



}





