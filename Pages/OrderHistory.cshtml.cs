using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using System.Security.Claims;

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

        public List<Order> Orders { get; set; } = new List<Order>();

        [TempData]
        public string? SuccessMessage { get; set; }
        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return RedirectToPage("/Login");
            }

            Orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Page();
        }

        // --- HÀM MỚI ĐỂ HỦY ĐƠN HÀNG ---
        public async Task<IActionResult> OnPostCancelAsync(int orderId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return RedirectToPage("/Login"); // Chua dang nhap
            }

            // Bat dau Giao dich (Transaction)
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Tim don hang (phai la cua user nay)
                    var order = await _context.Orders
                        .Include(o => o.OrderItems) // Lay cac san pham
                        .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

                    if (order == null)
                    {
                        ErrorMessage = "Không tìm thấy đơn hàng.";
                        return RedirectToPage();
                    }

                    // 2. Chi duoc huy neu dang "Cho van chuyen"
                    if (order.Status != "Đang chờ vận chuyển")
                    {
                        ErrorMessage = "Không thể hủy đơn hàng ở trạng thái này.";
                        return RedirectToPage();
                    }

                    // 3. HOAN KHO (Cong lai so luong)
                    foreach (var item in order.OrderItems)
                    {
                        var product = await _context.Products.FindAsync(item.ProductId);
                        if (product != null)
                        {
                            product.Stock += item.Quantity; // Cong tra lai so luong
                            _context.Products.Update(product);
                        }
                    }

                    // 4. Cap nhat trang thai don hang
                    order.Status = "Đã hủy";
                    _context.Orders.Update(order);

                    // 5. Luu tat ca thay doi (Hoan kho + Huy don)
                    await _context.SaveChangesAsync();

                    // 6. Hoan tat Giao dich
                    await transaction.CommitAsync();

                    SuccessMessage = $"Đã hủy thành công đơn hàng #{order.Id}.";
                }
                catch (Exception ex)
                {
                    // Neu co loi, huy bo tat ca
                    await transaction.RollbackAsync();
                    ErrorMessage = "Lỗi khi hủy đơn hàng: " + ex.Message;
                }
            }

            return RedirectToPage();
        }
    }
}