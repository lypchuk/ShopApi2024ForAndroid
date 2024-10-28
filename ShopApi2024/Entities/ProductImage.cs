using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi2024.Entities
{
    [Table("tblProductImages")]
    public class ProductImage
    {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Image { get; set; }

        public int Priority { get; set; }


        [ForeignKey("tblProducts")]
        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
