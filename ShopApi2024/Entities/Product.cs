using ShopApi2024.Interfaces;

namespace ShopApi2024.Entities
{
    public class Product : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? DeleteTime { get; set; }
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
