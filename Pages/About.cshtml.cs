using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoeShop.Pages
{
    // Trang này chủ yếu là nội dung tĩnh,
    // vì vậy file backend (logic) rất đơn giản.
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
            // Không cần logic gì khi tải trang
        }
    }
}