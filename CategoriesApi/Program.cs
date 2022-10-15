using CategoriesApi.Configurations;
using CategoriesApi.Configurations.Data;
using CategoriesApi.Models.Categories;
using CategoriesApi.Services.Categories;
using CategoriesApi.Services.Categories.ICategories;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// ConnectionStrings
builder.Services.Configure<ConnectionStrings>(config =>
{
    config.MSSQL = builder.Configuration.GetConnectionString("MSSQL");
});

// Add services to the container.
builder.Services.AddTransient<IDbContext, DbContext>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddControllers();
builder.Services.AddResponseCaching();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
