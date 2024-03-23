using uowpublic.Models; // Import the namespace for models
using uowpublic.Data;
using uowpublic.Services; // Import the namespace for services
using Microsoft.Extensions.DependencyInjection; // Import the namespace for dependency injection
using System.Diagnostics; // Import the namespace for debugging output
using Microsoft.EntityFrameworkCore; // Import the namespace for Entity Framework Core, used for database context

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = "server=localhost;database=unlife_public;user=root;password=root"; // MySQL database connection string

// Register the database context
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register services
builder.Services.AddScoped<IPropertyService, PropertyService>(); // Assuming you have a PropertyService

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