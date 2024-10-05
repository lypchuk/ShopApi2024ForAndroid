using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using ShopApi2024.Entities;
using ShopApi2024.Interfaces;
using ShopApi2024.Services;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;

namespace ShopApi2024
{
    public class ShopApi2024Db : DbContext 
    {
        //private readonly IWebHostEnvironment environment;
        public ShopApi2024Db()
        {
            //this.environment = environment;
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }

        public ShopApi2024Db(DbContextOptions options) : base(options)
        {
            //this.environment = environment;
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
            //Database.Migrate();//????????????
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    string connectionStr = "Data Source=DESKTOP-ASTVPAV;" +
        //        "Initial Catalog=ShopApi2024;" +
        //        "Integrated Security=True;" +
        //        "Connect Timeout=30;Encrypt=False;" +
        //        "Trust Server Certificate=False;" +
        //        "Application Intent=ReadWrite;" +
        //        "Multi Subnet Failover=False";

        //    optionsBuilder.UseSqlServer(connectionStr);

        //    optionsBuilder.EnableSensitiveDataLogging();
        //}
        //ShopApi2024

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            /*
            // after a delete
            db.Movies.Count(); // 9
            db.Movies.IgnoreQueryFilters().Count(); // 10
            */

            //var fakerCategory = new Faker<Category>()
            //    .RuleFor(u => u.Name, (f, u) => f.Commerce.Categories(1).FirstOrDefault()!)
            //    //.RuleFor(u => u.ImageName, (f, u) => f.Name.FirstName())
            //    //.RuleFor(u => u.Description, (f, u) => f.Lorem.Text())
            //    .RuleFor(u => u.CreationTime, (f, u) => new DateTime())
            //    ;

            /*
            int categoryCounter = 5;
            int productInCategory = 5;

            var faker = new Faker();

            Category[] categories = new Category[categoryCounter];
            Product[] products = new Product[categoryCounter*productInCategory];

            string[] CategoriesName = faker.Commerce.Categories(categoryCounter);

            for (int i = 0, a = 0; i < categoryCounter; i++)
            {
                categories[i] = new Category();
                categories[i].Id = i + 1;
                categories[i].Description = faker.Lorem.Text();
                //categories[i].ImageName = faker.Name.FirstName();
                //categories[i].ImagePath = Guid.NewGuid().ToString();
                categories[i].ImageName =  SaveImageFromUrl(imageUrl: "https://picsum.photos/300/300");
                categories[i].Name = CategoriesName[i];
                categories[i].CreationTime = DateTime.Now;
                //categories[i].Name = faker.Commerce.Categories(1).FirstOrDefault()!;

                for (int j = 0; j < productInCategory; j++, a++)
                {

                    products[a] = new Product();
                    products[a].Id = a + 1;
                    products[a].Name = faker.Commerce.ProductName();
                    products[a].ImageName = Guid.NewGuid().ToString();
                    products[a].Description = faker.Commerce.ProductDescription();
                    products[a].Price = Decimal.Parse( faker.Commerce.Price(min: 5, max: 1000));
                    products[a].Discount = faker.Random.Number(0, 50);
                    products[a].CategoryId = i+1;

                    products[a].CreationTime = DateTime.Now;
                }
            }
            */

            //modelBuilder.Entity<Category>().Property("IsDelete").HasDefaultValue(false);
            //modelBuilder.Entity<Product>().Property("IsDelete").HasDefaultValue(false);

            modelBuilder.Entity<Category>().HasQueryFilter(x=> x.IsDelete == false);
            modelBuilder.Entity<Product>().HasQueryFilter(x=> x.IsDelete == false);

            //modelBuilder.Entity<Category>().HasData(categories);
            //modelBuilder.Entity<Product>().HasData(products);

            //var f = new Faker();

            //Category cat = new Category();
            //cat.Name = f.Commerce.Categories(1).FirstOrDefault()!;


        }
    }
}


/*
 *package org.example.seeders;

@Component
public class ProductSeeder implements CommandLineRunner {

    private final CategoryRepository categoryRepository;
    private final StorageService storageService;
    private final Faker faker = new Faker();
    public ProductSeeder(StorageService storageService,CategoryRepository categoryRepository) {
        this.storageService = storageService;
        this.categoryRepository = categoryRepository;
    }

    @Override
    public void run(String... args) throws IOException {
        if (categoryRepository.count() == 0) {
            int categoryCount = 10;
            int min = 3;
            int max = 5;
            int productsPerCategoryCount = 5;
            int imageCount = categoryCount * (1 + (productsPerCategoryCount * max));
            ExecutorService executor = Executors.newFixedThreadPool(20);
            List<CompletableFuture<String>> imagesFutures = new ArrayList<>();
            for (int i = 0; i < imageCount; i++) {
                imagesFutures.add(
                        CompletableFuture.supplyAsync(() -> {
                            try {
                                return storageService.saveImages("https://picsum.photos/300/300", FileSaveFormat.WEBP);
                            } catch (IOException e) {
                                throw new RuntimeException(e);
                            }
                        }, executor)
                );
            }

            // Очікуємо завершення всіх завантажень зображень
            CompletableFuture<Void> allImages = CompletableFuture.allOf(imagesFutures.toArray(new CompletableFuture[0]));

            // Після завершення завантаження всіх зображень
            allImages.thenRun(() -> {
                List<String> imagesUrls = imagesFutures.parallelStream()
                        .map(CompletableFuture::join)
                        .toList();
                executor.shutdown();
                List<CategoryEntity> categories = new ArrayList<>();
                int imageIndex = 0;

                for (int i = 0; i < categoryCount; i++) {
                    // Створюємо нову категорію
                    CategoryEntity category = new CategoryEntity(
                            0,
                            faker.commerce().productName(),
                            imagesUrls.get(imageIndex++),
                            faker.lorem().sentence(10),
                            LocalDateTime.now(),
                            new ArrayList<>()
                    );

                    List<ProductEntity> products = new ArrayList<>();
                    for (int j = 0; j < productsPerCategoryCount; j++) {
                        // Створюємо новий продукт
                        ProductEntity product = new ProductEntity(
                                null,
                                faker.commerce().productName(),
                                faker.lorem().sentence(10),
                                LocalDateTime.now(),
                                faker.number().randomDouble(2, 10, 100),
                                faker.number().randomDouble(2, 0, 20),
                                category,
                                new ArrayList<>()
                        );


                        // Generate random integer between min (inclusive) and max (inclusive)
                        int randomMax = (int)(Math.random() * ((max - min) + 1)) + min;

                        List<ProductImageEntity> images = new ArrayList<>();
                        for (int k = 0; k < randomMax; k++) {
                            // Використовуємо наступне зображення для продукту
                            images.add(new ProductImageEntity(
                                    null,
                                    imagesUrls.get(imageIndex++),
                                    k,
                                    new Date(),
                                    false,
                                    product
                            ));
                        }

                        product.setProductImages(images);
                        products.add(product);
                    }

                    category.setProducts(products);
                    categories.add(category);
                }

                // Збереження категорій з продуктами і зображеннями в базу даних
                categoryRepository.saveAll(categories);
                System.out.println("Сид бази даних завершено!");
            }).exceptionally(ex -> {
                System.err.println("Помилка при збереженні категорій: " + ex.getMessage());
                return null;
            });


        }
    }

}
 */