using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;

namespace ShoeShop.Pages
{
    public class DetailModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; } = new Product();
        public bool ProductFound { get; set; } = false;

        // --- THÊM MỚI ---
        // Một danh sách mới để chứa các sản phẩm cùng loại
        public List<Product> RelatedProducts { get; set; } = new List<Product>();
        // --- KẾT THÚC THÊM ---

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                ProductFound = false;
                return Page();
            }

            Product = product;
            ProductFound = true;

            // --- THÊM LOGIC TÌM SẢN PHẨM CÙNG LOẠI ---
            // 1. Lấy danh mục của sản phẩm hiện tại (ví dụ: "Nam")
            var currentCategory = Product.Category;

            // 2. Tìm 4 sản phẩm khác trong CSDL
            RelatedProducts = await _context.Products
                .Where(p => p.Category == currentCategory && // Cùng danh mục
                              p.Id != id)                      // Nhưng khác ID (không lấy chính nó)
                .Take(4) // Chỉ lấy 4 sản phẩm
                .ToListAsync();
            // --- KẾT THÚC THÊM ---

            return Page();
        }
    }
}