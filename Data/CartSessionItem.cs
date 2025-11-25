namespace ShoeShop.Pages
{
    // Đây là class dùng chung cho cả Detail, Checkout và AddToCart
    public class CartSessionItem
    {
        public int ProductId { get; set; }
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}