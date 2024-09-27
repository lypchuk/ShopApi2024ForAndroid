namespace ShopApi2024.Interfaces
{
    public interface IFileService
    {
        //Task<string> SaveProductImages(IFormFile file);
        Task<string> UploadFileImage(IFormFile file);
        //string SaveCategoryImages(string imageUrl, string extension = ".webp");
        void DeleteFileImage(string imageName);
    }
}
