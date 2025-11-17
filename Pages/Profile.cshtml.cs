using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization; // <-- Can de bao ve trang
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using System.Security.Claims;

namespace ShoeShop.Pages
{
    [Authorize] // Bat buoc phai dang nhap moi vao duoc trang nay
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Bien de hien thi
        public User ProfileUser { get; set; } = new User();
        public int TotalOrders { get; set; } = 0;
        public bool UserFound { get; set; } = false;

        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Lay ID cua user dang dang nhap
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return RedirectToPage("/Login"); // Loi, khong tim thay ID
            }

            // 2. Tim user trong CSDL
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                UserFound = false;
                return Page(); // Van hien thi trang nhung bao loi
            }

            UserFound = true;
            ProfileUser = user; // Gan thong tin user

            // 3. Dem tong so don hang
            TotalOrders = await _context.Orders
                                    .Where(o => o.UserId == userId)
                                    .CountAsync(); // Dem so don hang cua user nay

            return Page();
        }
    }
}