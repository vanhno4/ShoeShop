using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeShop.Data
{
    // Bang nay se luu tru kho cho tung size
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Size { get; set; } = string.Empty; // "M", "L", "XL"

        [Required]
        public int Stock { get; set; } // So luong ton kho cho size nay

        [Required]
        public int ProductId { get; set; } // Khóa ngoại
        public Product? Product { get; set; }
    }
}