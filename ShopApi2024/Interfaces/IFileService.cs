namespace ShopApi2024.Interfaces
{
    public interface IFileService
    {
        //Task<string> SaveFileImage(IFormFile file);
        string SaveFileImage(IFormFile file);
        //string SaveCategoryImages(string imageUrl, string extension = ".webp");
        void DeleteFileImage(string ImagePath);
    }
}
