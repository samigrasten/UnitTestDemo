using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnitTestDemo.Database;
using UnitTestDemo.Repositories;

namespace UnitTestDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(
        IWeatherForecastRepository repository,
        ILogger<WeatherForecastController> logger)
        : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger = logger;

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
        {
            return await repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(int id)
        {
            var weatherForecast = await repository.GetByIdAsync(id);
            if (weatherForecast is null) return NotFound();

            return weatherForecast;
        }

        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> AddWheatherForecast(WeatherForecast weatherForecast)
        {
            await repository.AddAsync(weatherForecast);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyWeatherForecasts(int id, WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id) return BadRequest();
            if (!await repository.UpdateAsync(weatherForecast)) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecasts(int id)
        {
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
