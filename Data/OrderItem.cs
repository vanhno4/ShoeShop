using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // <-- Cần cái này

namespace ShoeShop.Data
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        // Chỉ định rõ: OrderId là khóa ngoại trỏ đến Order
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        // Chỉ định rõ: ProductId là khóa ngoại trỏ đến Product
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePaid { get; set; }

        [Required]
        public string Size { get; set; } = string.Empty;
    }
}