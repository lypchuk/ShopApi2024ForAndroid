using ShopApi2024.Entities;

namespace ShopApi2024.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? DeleteTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
