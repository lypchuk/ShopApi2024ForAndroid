using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShopApi2024;
using ShopApi2024.Data;
using ShopApi2024.Entities;
using ShopApi2024.Entities.Identity;
using ShopApi2024.Interfaces;
using ShopApi2024.Profiles;
using System.Linq;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



//string connStr = builder.Configuration.GetConnectionString("DefaultConnection")!;
string connStr = builder.Configuration.GetConnectionString("DefaultConnection")!;//sqllite
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Stores.MaxLengthForKeys = 128;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ShopApi2024DbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext(connStr);
builder.Services.AddRepositories();
builder.Services.AddCustomServices();

builder.Services.AddAutoMapper();
//builder.Services.AddAutoMapper(typeof(ApplicationProfile));


//builder.Services.AddScoped<IImageWorker, ImageWorker>();

//builder.Services.AddDbContext<ShopApi2024DbContext>(opt => opt.UseSqlServer(connStr));

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


app.UseCors(options =>
{
    options.WithOrigins("http://localhost:5041",
        "http://127.0.0.1:5041")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

var dir = builder.Configuration["ImagesDir"];
var productImages = builder.Configuration["ProductImages"];

var dirPath = Path.Combine(Directory.GetCurrentDirectory(), dir);
var dirPathProductImages = Path.Combine(Directory.GetCurrentDirectory(), productImages);

if (!Directory.Exists(dirPath))
{
    Directory.CreateDirectory(dirPath);
}

if (!Directory.Exists(dirPathProductImages))
{
    Directory.CreateDirectory(dirPathProductImages);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dirPath),
    RequestPath = "/uploadingImages"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dirPathProductImages),
    RequestPath = "/productImages"
});

var imageNo = Path.Combine(dirPath, "noimage.jpg");


if (!File.Exists(imageNo))
{
    string url = "https://m.media-amazon.com/images/I/71QaVHD-ZDL.jpg";
    try
    {
        using (HttpClient client = new HttpClient())
        {
            // Send a GET request to the image URL
            HttpResponseMessage response = client.GetAsync(url).Result;
            // Check if the response status code indicates success (e.g., 200 OK)
            if (response.IsSuccessStatusCode)
            {
                // Read the image bytes from the response content
                byte[] imageBytes = response.Content.ReadAsByteArrayAsync().Result;
                File.WriteAllBytes(imageNo, imageBytes);
            }
            else
            {
                Console.WriteLine($"------Failed to retrieve image. Status code: {response.StatusCode}---------");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"-----An error occurred: {ex.Message}------");
    }
}

//Dependecy Injection
app.SeedData();
//using (var scope = app.Services.CreateScope())
//{
//}




app.Run();
