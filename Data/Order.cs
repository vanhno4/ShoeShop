using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeShop.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // Liên kết với người dùng
        public User? User { get; set; } // Liên kết với người dùng

        [Required]
        public DateTime OrderDate { get; set; } // Thời gian thanh toán

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public string ShippingAddress { get; set; } = string.Empty; // Lưu địa chỉ

        [Required]
        public string PaymentMethod { get; set; } = string.Empty; // Lưu phương thức (COD/QR)

        // Một đơn hàng có NHIỀU sản phẩm
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}