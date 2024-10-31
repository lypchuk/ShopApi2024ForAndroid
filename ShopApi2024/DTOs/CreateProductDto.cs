using ShopApi2024.Entities;
using ShopApi2024.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopApi2024.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public IFormFile[]? Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }
        public int CategoryId { get; set; }
    }
}
