using ShopApi2024.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi2024.Entities
{
    [Table("tblProducts")]
    public class Product : ISoftDelete
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string[]? ImagePath { get; set; }

        [Required, StringLength(255)]
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public bool IsDelete { get; set; } = false;

        public DateTime? DeleteTime { get; set; }

        public decimal Price { get; set; }

        public double? Discount { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<ProductImage>? Images { get; set; }
    }
}
