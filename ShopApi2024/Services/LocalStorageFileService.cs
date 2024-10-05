using Microsoft.AspNetCore.Hosting.Server;
using ShopApi2024.Interfaces;
using System;
using System.IO;

namespace ShopApi2024.Services
{
    public class LocalStorageFileService(IConfiguration configuration, IWebHostEnvironment environment) : IFileService
    {
        private const string imagesFolder = "images";
        private const string wwwroot = "wwwroot";
        //private const string imageFolder = "C:/ImagesAspNetShopApi2024";
        //private readonly IWebHostEnvironment environment;

        //public LocalStorageFileService(IWebHostEnvironment environment)
        //{
        //    this.environment = environment;
        //}


        public void DeleteFileImage(string imagePath)
        {
            if (imagePath == "uploadingImages" + Path.DirectorySeparatorChar + "noimage.jpg")
            {
                return;
            }
            //string directory = Directory.GetCurrentDirectory();
            //string toimage = Path.Combine(directory, wwwroot, imagesFolder, imageName);
            var toimage = Path.Combine(Directory.GetCurrentDirectory(), imagePath);


            if (File.Exists(toimage))
            {
                File.Delete(toimage);
            }
        }

        public string SaveFileImage(IFormFile file)
        //public async Task<string> SaveFileImage(IFormFile file)
        {
            /*
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
            */

            if(file == null)
            {
                //return "uploadingImages" + Path.DirectorySeparatorChar + "noimage.jpg";
                return Path.Combine("uploadingImages", "noimage.jpg");
            }

            var dir = configuration["ImagesDir"];
            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), dir);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fullName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);    // random name


            string fileSave = Path.Combine(dirPath, fullName);

            using (var stream = new FileStream(fileSave, FileMode.Create))
                file.CopyToAsync(stream);


            //return fullName;//return image name


            // Path.AltDirectorySeparatorChar=/
            // Path.DirectorySeparatorChar=\
            // Path.PathSeparator=;
            // Path.VolumeSeparatorChar=:

            return Path.Combine(dir,fullName) ;//return image name
            //return "/" + dir + "/" + fullName;//return image name
        }        
    }
}
