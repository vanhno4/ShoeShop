using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data; // <-- Phải 'using' thư mục Data

namespace ShoeShop.Pages
{
    public class NamModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NamModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Biến này sẽ chứa 10 sản phẩm Nam
        public List<Product> Products { get; set; } = new List<Product>();

        // Hàm này chạy khi bạn vào trang /Nam
        public async Task OnGetAsync()
        {
            // Lấy TẤT CẢ sản phẩm Nam
            Products = await _context.Products
                                     .Where(p => p.Category == "Nam") // <-- Lọc theo "Nam"
                                     .ToListAsync();
        }
    }
}