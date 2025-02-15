using System;
using UnitTestDemo.Database;

namespace UnitTestDemo.Tests.Builders;

public class ForecastBuilder()
{
    private readonly WeatherForecast _forecast = new()
    {
        Id = 1,
        Date = DateOnly.FromDateTime(DateTime.Now),
        TemperatureC = 25,
        Summary = "Hot"
    };

    public ForecastBuilder WithId(int id)
    {   
        _forecast.Id = id;
        return this;
    }

    public ForecastBuilder WithDate(DateOnly date)
    {
        _forecast.Date = date;
        return this;
    }

    public ForecastBuilder WithTemperature(int temperature)
    {   
        _forecast.TemperatureC = temperature;
        return this;
    }

    public ForecastBuilder WithSummary(string summary)
    {   
        _forecast.Summary = summary;
        return this;
    }

    public WeatherForecast Build()
    {
        return _forecast;
    }
}