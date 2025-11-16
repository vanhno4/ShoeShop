using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using System.Security.Claims;

namespace ShoeShop.Pages
{
    [Authorize] // Phai dang nhap moi xem duoc
    public class OrderHistoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrderHistoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Danh sach cac don hang de hien thi
        public List<Order> Orders { get; set; } = new List<Order>();

        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Lay ID nguoi dung
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return RedirectToPage("/Login");
            }

            // 2. Lay tat ca don hang cua nguoi dung nay tu CSDL
            Orders = await _context.Orders
                .Where(o => o.UserId == userId)
                // Bao gom (Include) thong tin cac san pham trong don hang
                .Include(o => o.OrderItems)
                // Va bao gom (ThenInclude) thong tin cua chinh san pham do
                .ThenInclude(oi => oi.Product)
                // Sap xep don hang moi nhat len dau
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Page();
        }
    }
}