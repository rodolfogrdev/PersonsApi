using Business.Helpers;
using Business.Providers;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

//Add Business providers
builder.Services.AddScoped<IDatabaseManager, DatabaseManager>();
builder.Services.AddScoped<IGenericHelper, GenericHelper>();
builder.Services.AddScoped<IPersonProvider, PersonProvider>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}
