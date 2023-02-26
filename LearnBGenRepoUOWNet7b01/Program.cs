using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Context;
using MovieManagement.DataAccess.Implementation;
using MovieManagement.Domain.UnitOfWorks;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure dbContext for connectting db
builder.Services.AddDbContext<MovieManagementDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DeafautConnection"));
    }
    );

//Configure Unit of work for using repository
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//Fix over loop for join table
builder.Services.AddMvc()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
