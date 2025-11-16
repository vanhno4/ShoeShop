using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeShop.Data
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; } // Thuộc về Đơn hàng nào
        public Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; } // Là Sản phẩm nào
        public Product? Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePaid { get; set; } // Giá tại thời điểm mua
    }
}