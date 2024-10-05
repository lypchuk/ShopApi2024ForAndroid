using ShopApi2024.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi2024.Entities
{
    [Table("tblCategories")]
    public class Category : ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? ImagePath { get; set; }


        [Required, StringLength(255)]
        public string Description { get; set; }
        public bool IsDelete { get; set; } = false;
        public DateTime CreationTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }
}
