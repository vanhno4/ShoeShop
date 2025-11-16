using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using System; // <-- Cần thêm 'using' này để xáo trộn (Random)
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> BestSellers { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            // --- THAY ĐỔI LOGIC "BEST SELLER" ---
            // Yêu cầu: Lấy 2 sản phẩm từ mỗi danh mục

            // 1. Lấy 2 sản phẩm Nam
            var namProducts = await _context.Products
                                            .Where(p => p.Category == "Nam")
                                            .Take(2)
                                            .ToListAsync();

            // 2. Lấy 2 sản phẩm Nữ
            var nuProducts = await _context.Products
                                            .Where(p => p.Category == "Nữ")
                                            .Take(2)
                                            .ToListAsync();

            // 3. Lấy 2 sản phẩm Trẻ Em
            var treEmProducts = await _context.Products
                                            .Where(p => p.Category == "Trẻ Em")
                                            .Take(2)
                                            .ToListAsync();

            // 4. Lấy 2 sản phẩm Giảm Giá
            var giamGiaProducts = await _context.Products
                                            .Where(p => p.Category == "Giảm Giá")
                                            .Take(2)
                                            .ToListAsync();

            // 5. Gộp tất cả lại thành một danh sách (tổng cộng 8 sản phẩm)
            var combinedList = new List<Product>();
            combinedList.AddRange(namProducts);
            combinedList.AddRange(nuProducts);
            combinedList.AddRange(treEmProducts);
            combinedList.AddRange(giamGiaProducts);

            // 6. (Nên làm) Xáo trộn (Shuffle) danh sách này
            // để trang chủ không bị hiển thị theo thứ tự (2 Nam, 2 Nữ...)
            var random = new Random();
            BestSellers = combinedList.OrderBy(p => random.Next()).ToList();

            // Nếu bạn muốn giữ nguyên thứ tự (2 Nam, 2 Nữ...), 
            // thì dùng dòng này thay cho 2 dòng trên:
            // BestSellers = combinedList;
        }
    }
}