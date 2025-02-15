using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UnitTestDemo.Database;

namespace UnitTestDemo.Repositories;

public class WeatherForecastRepository(DatabaseContext context) : IWeatherForecastRepository
{
    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        return await context.WeatherForecasts.ToListAsync();
    }

    public async Task<WeatherForecast?> GetByIdAsync(int id)
    {
        return await context.WeatherForecasts.FindAsync(id);
    }

    public async Task AddAsync(WeatherForecast weatherForecast)
    {
        context.WeatherForecasts.Add(weatherForecast);
        await context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(WeatherForecast weatherForecast)
    {
        var originalForecast = await context.WeatherForecasts.FindAsync(weatherForecast.Id); 
        if (originalForecast == null) return false;

        originalForecast.Date = weatherForecast.Date;
        originalForecast.TemperatureC = weatherForecast.TemperatureC;
        originalForecast.Summary = weatherForecast.Summary;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var weatherForecast = await context.WeatherForecasts.FindAsync(id);
        if (weatherForecast == null) return;

        context.WeatherForecasts.Remove(weatherForecast);
        await context.SaveChangesAsync();
    }
}