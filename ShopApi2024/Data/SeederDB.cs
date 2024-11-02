using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopApi2024.Constants;
using ShopApi2024.Entities;
using ShopApi2024.Entities.Identity;
using ShopApi2024.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ShopApi2024.Data
{
    public static class SeederDB
    {
        private static readonly IConfiguration? configuration;
        public static async void  SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var imageWorker = scope.ServiceProvider.GetRequiredService<IImageWorker>();
                var dbContext = scope.ServiceProvider.GetRequiredService<ShopApi2024DbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

                dbContext.Database.Migrate(); //Запусти міграції на БД, якщо їх там немає

                var dir = configuration["ImagesDir"];//////////////////////////////////////////////////??????

                if (!dbContext.Categories.Any())
                {
                    const int number = 10;
                    int productInCategory = 5;
                    int productImagesCount = 3;
                    var categoriesName = new Faker("uk").Commerce.Categories(number);

                    var faker = new Faker();

                    //Parallel.ForEach(categoriesName, name => {
                    //    var entity = new Category
                    //    {
                    //        Name = name,
                    //        Description = faker.Lorem.Text(),
                    //        ImagePath = Path.Combine(dir, SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300")),
                    //        //ImagePath = dir + "/" + SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300"),
                    //        CreationTime = DateTime.UtcNow,
                    //        //CreationTime = DateTime.Now,
                    //    };

                    //    //entity.ImagePath = Path.Combine(dir, entity.ImageName);
                    //    //entity.ImagePath = "/" + dir + "/" + entity.ImageName;

                    //    dbContext.Categories.Add(entity);
                    //    dbContext.SaveChanges();
                    //});

                    foreach (var name in categoriesName)
                    {
                        var entity = new Category
                        {
                            Name = name,
                            Description = faker.Lorem.Text(),
                            ImagePath = Path.Combine(dir, SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300")),
                            //ImagePath = dir + "/" + SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300"),
                            CreationTime = DateTime.UtcNow,
                            //CreationTime = DateTime.Now,
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
                            CreationTime = DateTime.UtcNow,
                            //CreationTime = DateTime.Now,
                            Discount = faker.Random.Number(0, 50),
                            Price = Decimal.Parse(faker.Commerce.Price(min: 5, max: 1000))
                        };

                        string[] pathProdImag = { Path.Combine(dir, SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300")) };

                        Array.Resize(ref pathProdImag, pathProdImag.Length + 1);

                        pathProdImag[pathProdImag.Length - 1] = Path.Combine(dir, SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300"));

                        pathProdImag = [.. pathProdImag, Path.Combine(dir, SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300"))];

                        entity.ImagePath = pathProdImag;

                        //entity.ImagePath = Path.Combine(dir, entity.ImageName);
                        //entity.ImagePath ="/" + dir + "/" + entity.ImageName;

                        if (i % productInCategory == 0 && i > 0)
                        {
                            j++;
                        }
                        dbContext.Products.Add(entity);
                        dbContext.SaveChanges();
                    }


                    for (int i = 0; i < categoriesName.Length * productInCategory; i++)
                    {
                        for (int j = 0; j < productImagesCount; j++)
                        {
                            var entity = new ProductImage
                            {
                                //Image = SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300"),
                                //Image = await imageWorker.Save("https://picsum.photos/300/300"),
                                Image = await imageWorker.Save("https://picsum.photos/300/300"),
                                Priority = j + 1,
                                ProductId = i + 1,
                            };

                            dbContext.ProductImages.Add(entity);
                            dbContext.SaveChanges();
                        }
                    }
                }

                if (!dbContext.Roles.Any())
                {
                    foreach (var role in Roles.GetAll)
                    {
                        var result = roleManager.CreateAsync(new RoleEntity { Name = role }).Result;
                        if (!result.Succeeded)
                        {
                            Console.WriteLine($"--Error create Role {role}--");
                        }
                    }
                }

                if (!dbContext.Users.Any())
                {
                    string image = imageWorker.Save("https://picsum.photos/1200/800?person").Result;
                    var user = new UserEntity
                    {
                        Email = "admin@gmail.com",
                        UserName = "admin@gmail.com",
                        LastName = "Підкаблучник",
                        FirstName = "Іван",
                        Image = image
                    };
                    var result = userManager.CreateAsync(user, "123456").Result;
                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"--Problem create user--{user.Email}");
                    }
                    else
                    {
                        result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
                    }
                }
            }
        }
    
        private static string SaveImageFromUrl(string imageUrl, string extension = ".webp")
        {
            var dir = configuration["ImagesDir"];//////////////////////////////////////////////////??????
            
            //var dir = builder.Configuration["ImagesDir"];

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
    }
}
