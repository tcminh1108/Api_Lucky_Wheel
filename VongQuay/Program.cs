using Domain.Models;
using Microsoft.Extensions.Configuration;
using Repository.CategoryVoucher;
using Repository.User;
using Repository.VipPhones;
using Services.CategoryVoucher;
using Services.SendMail;
using Services.VipPhones;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDBContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<IVipPhonesRepository, VipPhonesRepository>();
builder.Services.AddScoped<IVipPhonesServices, VipPhonesServices>();
builder.Services.AddScoped<ISendMailServices, SendMailServices>();

builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("appsettings.Development.json", false, true)
    .Build();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:3000",
                                              "http://localhost:3000")
                                           .AllowAnyHeader()
                                                  .AllowAnyMethod();
                      });
});
//Add CORS
/*builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("https://localhost:3000");
}));*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
