using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;

namespace ShoeShop.Pages.Admin
{
    [Authorize(Roles = "Admin")] // Chi Admin moi xem duoc
    public class AllOrdersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AllOrdersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- DAY LA THUOC TINH BI THIEU ---
        // Property nay PHAI LA public de .cshtml co a
        public List<Order> Orders { get; set; } = new List<Order>();
        // --- KET THUC SUA ---

        public async Task<IActionResult> OnGetAsync()
        {
            // Lay tat ca don hang cua TAT CA user
            Orders = await _context.Orders
                .Include(o => o.User) // Kem thong tin User
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product) // Kem thong tin san pham
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Page();
        }
    }
}