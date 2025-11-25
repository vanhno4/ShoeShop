using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using Microsoft.AspNetCore.Authorization; // Bỏ comment nếu cần
using Microsoft.AspNetCore.Mvc; // Thêm dòng này để dùng TempData

namespace ShoeShop.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Các biến thống kê cũ
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalProducts { get; set; }
        public int PendingOrders { get; set; }

        // --- THÊM BIẾN NÀY: Danh sách sản phẩm ---
        public List<Product> RecentProducts { get; set; } = new List<Product>();

        [TempData]
        public string? SuccessMessage { get; set; } // Nhận thông báo từ trang AddProduct

        public async Task OnGetAsync()
        {
            // 1. Đếm tổng số đơn hàng
            TotalOrders = await _context.Orders.CountAsync();

            // 2. Tính tổng doanh thu (SỬA LẠI ĐOẠN NÀY)
            // Thay vì SumAsync trực tiếp, ta lấy list số tiền về rồi Sum bên C#
            var revenueList = await _context.Orders
                .Where(o => o.Status != "Đã hủy")
                .Select(o => o.TotalAmount)
                .ToListAsync();

            TotalRevenue = revenueList.Sum(); // C# tính tổng -> Không bao giờ lỗi

            // 3. Đếm tổng sản phẩm đang bán
            TotalProducts = await _context.Products.CountAsync();

            // 4. Đếm đơn đang chờ xử lý
            PendingOrders = await _context.Orders
                .CountAsync(o => o.Status == "Đang chờ vận chuyển");

            // 5. Lấy danh sách sản phẩm mới nhất
            RecentProducts = await _context.Products
                .OrderByDescending(p => p.Id)
                .Take(10)
                .ToListAsync();
        }
    }
}