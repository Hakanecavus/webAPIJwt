using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace teemUpAPIv2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize] Class'? auth yapmak i?in
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]//, Authorize(Roles ="Admin")]  //E?er auth yap?lan bir class da auth istemedi?in bir method varsa class'? auth yap?p fonksiyonu AllowAnonymous kullan
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}