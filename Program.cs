using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHybridCache();
builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.ConfigurationOptions = new()
        {
            EndPoints = [],
            Password = "p4ssw0rd"
        };

        options.ConfigurationOptions.EndPoints.Add("localhost:6379");
    });

var app = builder.Build();

var hybridCache = app.Services.GetRequiredService<HybridCache>();

Console.WriteLine("Will get");
await hybridCache.GetOrCreateAsync(args[0], async (_) =>
{
    Console.WriteLine("Creating");
    await Task.Delay(TimeSpan.FromHours(1));
    return "ok";
});
