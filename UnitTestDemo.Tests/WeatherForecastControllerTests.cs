using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using UnitTestDemo.Controllers;
using UnitTestDemo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UnitTestDemo.Database;
using UnitTestDemo.Tests.Builders;

namespace UnitTestDemo.Tests;

// Installed Nuget packages:
// XUnit
// Moq
// Microsoft.EntityFrameworkCore.InMemory
// Microsoft.NET.Test.Sdk

public class WeatherForecastControllerTests : IDisposable
{
    private readonly WeatherForecastController _sut;
    private readonly DatabaseContext _context;
    private readonly WeatherForecast _weatherForecastInDatabase = new ForecastBuilder().Build();

    public WeatherForecastControllerTests()
    {
        var weatherForecast = new ForecastBuilder().WithId(2).Build();
        _context = new DatabaseContextBuilder()
            .WithData(_weatherForecastInDatabase)
            .WithData(weatherForecast)
            .Build();
        IWeatherForecastRepository repository = new WeatherForecastRepository(_context);
        //Mock<ILogger<WeatherForecastController>> mockLogger = new();
        _sut = new WeatherForecastController(repository, new FakeLogger());
    }

    [Fact]
    public async Task ShouldReturnsWeatherForecasts()
    {
        // Act
        var result = await _sut.GetWeatherForecasts();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ShouldReturnsNotFound_WhenForecastDoesNotExist()
    {
        // Arrange
        const int notExistingIdForForecast = 999;

        // Act
        var result = await _sut.GetWeatherForecast(notExistingIdForForecast);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Post_AddsWeatherForecast()
    {
        // Arrange
        var weatherForecastRequest = new ForecastBuilder().WithId(3).Build();
        
        // Act
        var result = await _sut.AddWheatherForecast(weatherForecastRequest);

        // Assert
        Assert.IsType<CreatedResult>(result.Result);
    }

    [Fact]
    public async Task Put_UpdatesWeatherForecast()
    {
        const int expectedTemperature = 15;
        const string expectedSummary = "Cold";
        var weatherForecastRequest = new ForecastBuilder()
            .WithId(_weatherForecastInDatabase.Id)
            .WithDate(_weatherForecastInDatabase.Date)
            .WithTemperature(expectedTemperature)
            .WithSummary(expectedSummary)
            .Build();

        // Act
        var result = await _sut.ModifyWeatherForecasts(weatherForecastRequest.Id, weatherForecastRequest);

        // Assert
        Assert.IsType<NoContentResult>(result);
        var updatedWeatherForecast = await _context.WeatherForecasts.FindAsync(weatherForecastRequest.Id);
        Assert.Equal(expectedTemperature, updatedWeatherForecast!.TemperatureC);
    }

    [Fact]
    public async Task Delete_RemovesWeatherForecast()
    {
        // Act
        var result = await _sut.DeleteWeatherForecasts(_weatherForecastInDatabase.Id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        var deletedWeatherForecast = await _context.WeatherForecasts.FindAsync(_weatherForecastInDatabase.Id);
        Assert.Null(deletedWeatherForecast);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}



public class FakeLogger : ILogger<WeatherForecastController>
{
    private int counter = 0;
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        counter++;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }
}