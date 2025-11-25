using ShoeShop.Data;

namespace ShoeShop.Pages
{
    // Class này dùng để hiển thị thông tin giỏ hàng kèm giá tiền
    public class CartViewModel
    {
        public Product Product { get; set; } = new Product();
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }

        // Tính toán tổng tiền
        public decimal TotalPrice => Product.Price * Quantity;
        public decimal? OriginalTotalPrice => Product.OriginalPrice * Quantity;
    }
}