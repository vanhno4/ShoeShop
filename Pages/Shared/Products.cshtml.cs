using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using Microsoft.AspNetCore.Authorization;

namespace ShoeShop.Pages.Admin
{
    // [Authorize(Roles = "Admin")]
    public class ProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ProductsModel(ApplicationDbContext context) => _context = context;

        public List<Product> ProductList { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            // Lấy danh sách sản phẩm, sắp xếp mới nhất lên đầu
            ProductList = await _context.Products
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }
    }
}