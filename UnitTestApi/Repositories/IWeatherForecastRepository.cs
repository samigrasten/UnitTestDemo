using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTestDemo.Database;

namespace UnitTestDemo.Repositories;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
    Task<WeatherForecast?> GetByIdAsync(int id);
    Task AddAsync(WeatherForecast weatherForecast);
    Task<bool> UpdateAsync(WeatherForecast weatherForecast);
    Task DeleteAsync(int id);
}