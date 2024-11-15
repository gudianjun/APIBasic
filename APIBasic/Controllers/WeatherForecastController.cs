
using APIBasic.Common;
using APIBasic.Enums;
using APIBasic.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBasic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]Å@
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly MySqlDbContext _context;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, MySqlDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize(Roles = Roles.User)]
        public IActionResult Get()
        { 
            //throw new Exception("This is a test exception.");
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            var response = new ApiResponse<IEnumerable<WeatherForecast>>(forecasts);
            return response.Result();
        }
    }

}
