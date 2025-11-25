using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoeShop.Pages
{
    public class ContactModel : PageModel
    {
        // Biến này để kiểm tra xem đã gửi tin nhắn chưa
        public bool IsSent { get; set; } = false;

        [BindProperty]
        public string Name { get; set; } = string.Empty; // Giữ lại thông tin để chào hỏi nếu cần

        public void OnGet()
        {
            IsSent = false;
        }

        public void OnPost()
        {
            // Xử lý gửi mail (giả lập)
            // ...

            // Bật cờ thành công để giao diện thay đổi
            IsSent = true;
        }
    }
}