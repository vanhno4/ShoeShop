using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;

namespace ShoeShop.Pages
{
    public class NuModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NuModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            // Lọc sản phẩm "Nữ"
            Products = await _context.Products
                                     .Where(p => p.Category == "Nữ")
                                     .ToListAsync();
        }
    }
}