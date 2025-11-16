using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;

namespace ShoeShop.Pages
{
    public class TreEmModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TreEmModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            // Chỉ khác ở đây: Lọc "Trẻ Em"
            Products = await _context.Products
                                     .Where(p => p.Category == "Trẻ Em")
                                     .ToListAsync();
        }
    }
}