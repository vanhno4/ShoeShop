using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ShoeShop.Pages
{
    [Authorize] // Phải đăng nhập mới vào được
    public class OrderSuccessModel : PageModel
    {
        public void OnGet()
        {
            // Trang này chỉ để hiển thị, không cần logic
        }
    }
}