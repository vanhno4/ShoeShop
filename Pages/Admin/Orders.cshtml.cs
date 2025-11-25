using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using Microsoft.AspNetCore.Authorization;

namespace ShoeShop.Pages.Admin
{
    // [Authorize(Roles = "Admin")] 
    public class OrdersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrdersModel(ApplicationDbContext context) => _context = context;

        public List<Order> Orders { get; set; } = new List<Order>();

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product) // <--- DÒNG QUAN TRỌNG NHẤT ĐỂ LẤY ẢNH
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}