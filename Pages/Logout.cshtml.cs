using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ShoeShop.Pages
{
    public class LogoutModel : PageModel
    {
        // Hàm OnGet sẽ chạy ngay khi bạn truy cập /Logout
        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Xóa cookie đăng nhập (như cũ)
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            // --- DÒNG MỚI ĐƯỢC THÊM ---
            // 2. Xóa giỏ hàng khỏi Session
            HttpContext.Session.Remove("Cart");
            // --- KẾT THÚC THÊM ---

            // 3. Chuyển về Trang Chủ (như cũ)
            return RedirectToPage("/Index");
        }
    }
}