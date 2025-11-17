using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeShop.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OriginalPrice { get; set; }

        [Required]
        public int Stock { get; set; } // Truong "Stock" cu, bay gio se la TONG ton kho

        // --- DÒNG MỚI ĐÃ THÊM ---
        // Mot san pham se co NHIEU bien the (size)
        public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        // --- KẾT THÚC THÊM ---
    }
}