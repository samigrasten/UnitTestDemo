using Microsoft.EntityFrameworkCore;
using UnitTestDemo.Database;

namespace UnitTestDemo.Tests.Builders;

public class DatabaseContextBuilder
{
    private DatabaseContext _context;

    public DatabaseContextBuilder ()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "WeatherForecastTest")
            .Options;
        _context = new DatabaseContext(options);
    }

    public DatabaseContextBuilder WithData<T>(T entity) where T : class
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return this;
    }

    public DatabaseContext Build()
    {
        _context.SaveChanges();
        return _context;
    }
}