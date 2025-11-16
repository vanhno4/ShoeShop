using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization; // <-- Cần để phân quyền
using ShoeShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ShoeShop.Pages.Admin
{
    [Authorize(Roles = "Admin")] // <-- Chỉ Admin mới vào được
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            // Lấy tất cả sản phẩm
            Products = await _context.Products.OrderBy(p => p.Id).ToListAsync();
        }
    }
}