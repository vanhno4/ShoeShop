using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeShop.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        // --- ĐÂY LÀ DÒNG QUAN TRỌNG ---
        [Required]
        public string Status { get; set; } = string.Empty;
        // --- KẾT THÚC ---

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}