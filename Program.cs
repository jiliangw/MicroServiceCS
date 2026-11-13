using Microsoft.EntityFrameworkCore;
using platformService.Data;
using Platformservice.Data;
using PlatformService.Data;
using PlatformService.SyncdataService.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseInMemoryDatabase("InMem");
        Console.WriteLine  ("using InMemo DB");
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("PlatoformsConn"));
        Console.WriteLine  ("using SQL DB");
    }
});
    
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddControllers();
builder.Services.AddHttpClient<ICommandDataClient,HttpCommandDataClient>();

Console.WriteLine($"----> CommandService Endpoint {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();




var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapControllers();

PrepDb.prepPopulation(app,app.Environment.IsProduction());

app.Run();



record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
