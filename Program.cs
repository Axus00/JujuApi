using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Application.DTOs;
using ProjectApi.Domain.Entities;
using ProjectApi.Domain.Services.Interfaces;
using ProjectApi.Domain.Validators.Customers;
using ProjectApi.Domain.Validators.Posts;
using ProjectApi.Infrastructure.ExternalServices;
using ProjectApi.Infrastructure.Persistence.Repository.Customers;
using ProjectApi.Infrastructure.Persistence.Repository.Posts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuración contexto
builder.Services.AddDbContext<SqlServerContext>(options => 
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("SqlConnection")));

//otros servicios
builder.Services.AddControllers();

//servicio e interfaces
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IPost, PostService>();

//validator 
builder.Services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();
builder.Services.AddScoped<IValidator<PostDto>, PostValidator>();
builder.Services.AddScoped<IValidator<PostsDto>, PostsValidator>();

//Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
