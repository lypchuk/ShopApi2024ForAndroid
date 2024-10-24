using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShopApi2024;
using ShopApi2024.Entities;
using ShopApi2024.Profiles;
using System.Linq;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



//string connStr = builder.Configuration.GetConnectionString("LocalDb")!;
string connStr = builder.Configuration.GetConnectionString("DefaultConnection")!;//sqllite
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
//builder.Services.AddAutoMapper(typeof(ApplicationProfile));




//builder.Services.AddDbContext<ShopApi2024Db>(opt => opt.UseSqlServer(connStr));

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
var dirPath = Path.Combine(Directory.GetCurrentDirectory(), dir);
if (!Directory.Exists(dirPath))
{
    Directory.CreateDirectory(dirPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dirPath),
    RequestPath = "/uploadingImages"
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
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShopApi2024Db>();
    dbContext.Database.Migrate(); //Запусти міграції на БД, якщо їх там немає

    if (!dbContext.Categories.Any())
    {
        const int number = 10;
        int productInCategory = 5;
        var categoriesName = new Faker("uk").Commerce.Categories(number);

        var faker = new Faker();

        foreach (var name in categoriesName)
        {
            var entity = new Category
            {
                Name = name,
                Description = faker.Lorem.Text(),
                ImagePath = Path.Combine(dir , SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300")),
                //ImagePath = dir + "/" + SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300"),
                CreationTime = DateTime.Now,
            };

            //entity.ImagePath = Path.Combine(dir, entity.ImageName);
            //entity.ImagePath = "/" + dir + "/" + entity.ImageName;

            dbContext.Categories.Add(entity);
            dbContext.SaveChanges();
        }

        for (int i = 0, j = 1; i < categoriesName.Length * productInCategory; i++)
        {
            var entity = new Product
            {
                Name = faker.Commerce.ProductName(),
                CategoryId = j,
                Description = faker.Commerce.ProductDescription(),
                //ImageName = Guid.NewGuid().ToString(),
                 
                //ImagePath = [.. (Path.Combine(dir, SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300")))],
                //ImagePath = dir + "/" + SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300"),
                //ImagePath = "no",
                CreationTime = DateTime.Now,
                Discount = faker.Random.Number(0, 50),
                Price = Decimal.Parse(faker.Commerce.Price(min: 5, max: 1000))
            };

            string[] pathProdImag = { Path.Combine(dir, SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300")) };

            Array.Resize(ref pathProdImag, pathProdImag.Length + 1);

            pathProdImag[pathProdImag.Length-1] = (Path.Combine(dir, SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300")));

            pathProdImag = [.. pathProdImag, Path.Combine(dir, SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300"))];

            entity.ImagePath = pathProdImag;

            //entity.ImagePath = Path.Combine(dir, entity.ImageName);
            //entity.ImagePath ="/" + dir + "/" + entity.ImageName;

            if (i % productInCategory == 0 && i>0)
            {
                j++;
            }
            dbContext.Products.Add(entity);
            dbContext.SaveChanges();
        }
    }

   

}


string SaveImageFromUrl(string imageUrl, string extension = ".webp")
{
    var dir = builder.Configuration["ImagesDir"];
    var dirPath = Path.Combine(Directory.GetCurrentDirectory(), dir);

    //if (!Directory.Exists(dirPath))
    //{
    //    Directory.CreateDirectory(dirPath);
    //}

    string name = Guid.NewGuid().ToString();    // random name
    string extensionFn = extension;// ".webp"; // get original extension
    string fullName = name + extensionFn;


    //string path = Directory.GetCurrentDirectory() + "/wwwroot/";

    string imageFullPath = Path.Combine(dirPath, fullName);

    using (System.Net.WebClient client = new System.Net.WebClient())
    {
        //client.DownloadFile(new Uri(imageUrl), imageFullPath);
        client.DownloadFileAsync(new Uri(imageUrl), imageFullPath);
    }

    return fullName;//return image name
}

app.Run();
