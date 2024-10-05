using ShopApi2024.Entities;

namespace ShopApi2024.DTOs
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string ImagePath { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
