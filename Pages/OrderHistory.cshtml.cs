using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ShoeShop.Pages
{
    [Authorize]
    public class OrderHistoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrderHistoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tên biến thống nhất là Orders
        public List<Order> Orders { get; set; } = new List<Order>();

        [TempData]
        public string? SuccessMessage { get; set; }
        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdString, out var userId))
            {
                // QUAN TRỌNG: Cấu trúc lệnh này giúp lấy đầy đủ Ảnh và Tên
                Orders = await _context.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.OrderItems)          // 1. Lấy chi tiết đơn
                        .ThenInclude(oi => oi.Product)   // 2. KẾT NỐI VỚI BẢNG SẢN PHẨM (Để lấy Ảnh, Tên)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();
            }
        }

        // Hàm Hủy Đơn Hàng (Đã tích hợp hoàn kho Size)
        public async Task<IActionResult> OnPostCancelAsync(int orderId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId)) return RedirectToPage("/Login");

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var order = await _context.Orders
                        .Include(o => o.OrderItems)
                        .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

                    if (order == null || order.Status != "Đang chờ vận chuyển")
                    {
                        ErrorMessage = "Không thể hủy đơn hàng này.";
                        return RedirectToPage();
                    }

                    // Hoàn kho
                    foreach (var item in order.OrderItems)
                    {
                        // 1. Hoàn kho Variant (Size)
                        var variant = await _context.ProductVariants
                            .FirstOrDefaultAsync(v => v.ProductId == item.ProductId && v.Size == item.Size);
                        if (variant != null)
                        {
                            variant.Stock += item.Quantity;
                            _context.ProductVariants.Update(variant);
                        }

                        // 2. Hoàn kho Tổng (Product)
                        var product = await _context.Products.FindAsync(item.ProductId);
                        if (product != null)
                        {
                            product.Stock += item.Quantity;
                            _context.Products.Update(product);
                        }
                    }

                    order.Status = "Đã hủy";
                    _context.Orders.Update(order);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    SuccessMessage = $"Đã hủy đơn hàng #{order.Id}.";
                }
                catch
                {
                    await transaction.RollbackAsync();
                    ErrorMessage = "Lỗi hệ thống khi hủy đơn.";
                }
            }
            return RedirectToPage();
        }
    }
}