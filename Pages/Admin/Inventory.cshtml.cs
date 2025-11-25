using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using Microsoft.AspNetCore.Authorization;

namespace ShoeShop.Pages.Admin
{
    // [Authorize(Roles = "Admin")]
    public class InventoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InventoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            // Lấy sản phẩm kèm Variants (Size) để đếm số lượng
            Products = await _context.Products
                .Include(p => p.Variants)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}