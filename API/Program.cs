        using System.Text;
        using Api.Data;
using Api.Middleware;
using API.Extensions;
        using API.Interfaces;
        using API.Services;
        using DatingApp;
        using Microsoft.AspNetCore.Authentication.JwtBearer;
        using Microsoft.EntityFrameworkCore;
        using Microsoft.IdentityModel.Tokens;

        var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();
                builder.Services.AddControllers();
            
                builder.Services.AddApplicationServices(builder.Configuration);
                builder.Services.AddIdentityServices(builder.Configuration);
                
                var app = builder.Build();
                app.UseMiddleware<ExceptionMiddleware>();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

                app.UseHttpsRedirection();
        app.UseCors(builder =>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
        app.UseAuthentication();
        app.UseAuthorization();
            app.MapControllers();
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

                app.Run();

                record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
                {
                    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
                }
