using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApi2024.Interfaces;
using ShopApi2024.Profiles;
using ShopApi2024.Repositories;
using ShopApi2024.Services;

namespace ShopApi2024
{
    public static class ServiceExtensions
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<ShopApi2024Db>(opts => opts.UseSqlServer(connectionString));
            services.AddDbContext<ShopApi2024Db>(opts => opts.UseSqlite(connectionString));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFileService, LocalStorageFileService>();


        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            //services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile(provider.CreateScope().ServiceProvider.GetService<IFileService>()!));
            }).CreateMapper());
        }

        //public static void AddAutoMapper(this IServiceCollection services)
        //{
        //    services.AddSingleton(provider => new MapperConfiguration(cfg =>
        //    //services.AddScoped(provider => new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile(new ApplicationProfile(provider.CreateScope().ServiceProvider.GetService<IFileService>()!));
        //    }).CreateMapper());
        //}
    }
}
