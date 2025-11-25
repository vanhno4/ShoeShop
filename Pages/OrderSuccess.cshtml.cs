using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using ShoeShop.Pages; // <-- Để dùng class CartSessionItem (hoặc tạo class ViewModel riêng)

namespace ShoeShop.Pages
{
    // Tạo class trùng khớp với dữ liệu đã lưu ở Checkout
    public class OrderSuccessViewModel
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Size { get; set; } = string.Empty;
    }

    [Authorize]
    public class OrderSuccessModel : PageModel
    {
        public List<OrderSuccessViewModel> OrderItems { get; set; } = new List<OrderSuccessViewModel>();

        public IActionResult OnGet()
        {
            // 1. Đọc dữ liệu đơn hàng vừa lưu trong Session
            var lastOrderJson = HttpContext.Session.GetString("LastOrder");

            if (string.IsNullOrEmpty(lastOrderJson))
            {
                // Nếu không có đơn hàng nào mới, quay về trang chủ
                return RedirectToPage("/Index");
            }

            try
            {
                // 2. Giải mã JSON thành danh sách
                OrderItems = JsonSerializer.Deserialize<List<OrderSuccessViewModel>>(lastOrderJson) ?? new List<OrderSuccessViewModel>();
            }
            catch
            {
                return RedirectToPage("/Index");
            }

            // (Tùy chọn) Xóa Session này đi để nếu F5 lại sẽ không hiện nữa (tránh duplicate hiển thị)
            // HttpContext.Session.Remove("LastOrder");

            return Page();
        }
    }
}