namespace ShopApi2024.DTOs
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        //public string ImageName { get; set; }
        public string Description { get; set; }
    }
}
