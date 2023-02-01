//tao import global trong project
global using LearnBasApiNet7b01.Models;
using LearnBasApiNet7b01.Data;
using LearnBasApiNet7b01.Services.SuperHeroService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add service 
builder.Services.AddTransient<ISuperHeroService, SuperHeroService>();

//add connection string with entity frame
// C1: add chuoi connection string trong DbContext
builder.Services.AddDbContext<DataContext>();

/**
 * C2: lay chuoi connection string trong application.json
 */
/*builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnect"))
});*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
