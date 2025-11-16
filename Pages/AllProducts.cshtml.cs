using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeShop.Pages
{
    public class AllProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AllProductsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            // Lấy tất cả 32 sản phẩm
            Products = await _context.Products.ToListAsync();
        }
    }
}