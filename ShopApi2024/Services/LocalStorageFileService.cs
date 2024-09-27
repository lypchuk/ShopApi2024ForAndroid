using Microsoft.AspNetCore.Hosting.Server;
using ShopApi2024.Interfaces;
using System;

namespace ShopApi2024.Services
{
    public class LocalStorageFileService : IFileService
    {
        private const string imagesFolder = "images";
        private const string wwwroot = "wwwroot";
        //private const string imageFolder = "C:/ImagesAspNetShopApi2024";
        private readonly IWebHostEnvironment environment;

        public LocalStorageFileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }


        public void DeleteFileImage(string imageName)
        {
            string directory = Directory.GetCurrentDirectory();
            string toimage = Path.Combine(directory, wwwroot, imagesFolder, imageName);


            if (File.Exists(toimage))
            {
                File.Delete(toimage);
            }
        }

        public async Task<string> UploadFileImage(IFormFile file)
        {
            // get image destination path
            string root = environment.WebRootPath;      // wwwroot
            string name = Guid.NewGuid().ToString();    // random name
            string extension = Path.GetExtension(file.FileName); // get original extension
            string fullName = name + extension;         // full name: name.ext

            // create destination image file path
            string imagePath = Path.Combine(imagesFolder, fullName);
            string imageFullPath = Path.Combine(root, imagePath);

            // save image on the folder
            using (FileStream fs = new FileStream(imageFullPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            // return image file path
            //return Path.DirectorySeparatorChar + imagePath;//
            return fullName;
        }

        //private Task<string> SaveCategoryImages(string imageUrl, string extension = ".webp")
        //{
        //    string name = Guid.NewGuid().ToString();    // random name
        //    string extensionFn = extension;// ".webp"; // get original extension
        //    string fullName = name + extensionFn;


        //    string path = Directory.GetCurrentDirectory() + "/wwwroot/";
        //    string imageFolder = "images";


        //    string imagePath = Path.Combine(imagesFolder, fullName);
        //    string imageFullPath = Path.Combine(path, imagePath);

        //    if (!System.IO.Directory.Exists(Path.Combine(path, imagesFolder)))
        //    {
        //        System.IO.Directory.CreateDirectory(Path.Combine(path, imageFolder));
        //    }

        //    using (System.Net.WebClient client = new System.Net.WebClient())
        //    {
        //        //client.DownloadFile(new Uri(imageUrl), imageFullPath);
        //        client.DownloadFile(new Uri(imageUrl), imageFullPath);
        //    }

        //    //return Task.FromResult(path + fullName);
        //    return Task.FromResult(Path.DirectorySeparatorChar + imagePath);
        //}




        /*
        public async Task<string> SaveProductImages(IFormFile file)
        {
            // get image destination path
            string root = environment.WebRootPath;      // wwwroot
            string name = Guid.NewGuid().ToString();    // random name
            string extension = Path.GetExtension(file.FileName); // get original extension
            string fullName = name + extension;         // full name: name.ext

            // create destination image file path
            string imagePath = Path.Combine(imagesFolder, fullName);
            string imageFullPath = Path.Combine(root, imagePath);

            // save image on the folder
            using (FileStream fs = new FileStream(imageFullPath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            // return image file path
            return Path.DirectorySeparatorChar + imagePath;
        }
        */
    }
}
