using OpenWeather.Application;
using OpenWeather.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenWeatherDependencies();
builder.Services.AddTransient<ExceptionHandlerMiddleware>();

builder.Services.AddCors(
    options => options.AddPolicy(name: "ApiOriginsPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:80",
                               "http://localhost:4200",
                               "https://localhost:443")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        }
    )
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlerMiddleware();

app.UseCors("ApiOriginsPolicy");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
