using ShopApi2024.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopApi2024.DTOs
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int Priority { get; set; }
        public int ProductId { get; set; }
    }
}
