using ShopApi2024.Entities;

namespace ShopApi2024.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string ImageName { get; set; }
        public string[] ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        //public int[]? ProductImageId { get; set; }
        public ICollection<ProductImageDto>? Images { get; set; }
    }

}
