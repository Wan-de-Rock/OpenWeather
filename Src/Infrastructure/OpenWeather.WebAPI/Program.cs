using OpenWeather.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenWeatherDependencies();

builder.Services.AddCors(
    options => options.AddPolicy(name: "ApiOriginsPolicy",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:80",
                               "http://172.17.0.1:80",
                               "http://localhost:80",
                               "http://localhost:443",
                               "http://172.17.0.1:4200",
                               "http://localhost:4200/",
                               "http://localhost:5200/",
                               "http://172.17.0.1:5200")
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

app.UseCors("ApiOriginsPolicy");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
