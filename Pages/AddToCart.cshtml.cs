using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json; // <-- Cần để dùng JSON

namespace ShoeShop.Pages
{
    [Authorize] // Bắt buộc phải đăng nhập mới vào được trang này
    public class AddToCartModel : PageModel
    {
        // Hàm OnGet sẽ chạy khi bạn truy cập /AddToCart?id=...
        public IActionResult OnGet(int id)
        {
            // 1. Lấy giỏ hàng cũ từ Session
            List<int> cart;
            var cartJson = HttpContext.Session.GetString("Cart");

            if (string.IsNullOrEmpty(cartJson))
            {
                // Nếu chưa có giỏ hàng, tạo mới
                cart = new List<int>();
            }
            else
            {
                // Nếu có rồi, chuyển JSON thành List
                cart = JsonSerializer.Deserialize<List<int>>(cartJson) ?? new List<int>();
            }

            // 2. Thêm ID sản phẩm mới vào giỏ hàng
            // (Chúng ta cho phép thêm trùng lặp để tính số lượng sau)
            cart.Add(id);

            // 3. Lưu giỏ hàng mới vào Session
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));

            // 4. Chuyển hướng đến trang Thanh Toán
            return RedirectToPage("/Checkout");
        }
    }
}