using Microsoft.EntityFrameworkCore;
using ShopApi2024;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



string connStr = builder.Configuration.GetConnectionString("LocalDb")!;
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext(connStr);
builder.Services.AddRepositories();
builder.Services.AddCustomServices();

builder.Services.AddAutoMapper();




//builder.Services.AddDbContext<ShopApi2024Db>(opt => opt.UseSqlServer(connStr));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200",
        "http://localhost:3002",
        "http://localhost:5041",
        "http://localhost:3001",
        "http://localhost:3000",
        "https://jolly-pebble-03ffb7210.5.azurestaticapps.net/",
        "https://jolly-pebble-03ffb7210.5.azurestaticapps.net",
        "http://localhost:55756")
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
