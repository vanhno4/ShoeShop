using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ShoeShop.Pages
{
    // (Class CartItemViewModel van o day)
    public class CartItemViewModel
    {
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;
        public decimal? OriginalTotalPrice => Product.OriginalPrice * Quantity;
    }

    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CheckoutModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // (Cac thuoc tinh giu nguyen)
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
        public decimal Subtotal { get; set; } = 0;
        public decimal Shipping { get; set; } = 30000;
        public decimal Total { get; set; } = 0;
        public decimal TotalSavings { get; set; } = 0;

        [TempData]
        public string? ErrorMessage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string Telephone { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string Address { get; set; } = string.Empty;

        [BindProperty]
        public string PaymentMethod { get; set; } = "cod";

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadCart();
            if (!CartItems.Any())
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        // --- SUA LAI HAM ONPOSTASYNC ---
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCart();
                return Page();
            }

            await LoadCart();
            if (!CartItems.Any())
            {
                return RedirectToPage("/Index");
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return RedirectToPage("/Login");
            }

            // Bat dau Giao dich CSDL
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Kiem tra ton kho
                    var productIds = CartItems.Select(c => c.Product.Id).ToList();
                    var productsInDb = await _context.Products
                        .Where(p => productIds.Contains(p.Id))
                        .ToListAsync();

                    foreach (var item in CartItems)
                    {
                        var dbProduct = productsInDb.FirstOrDefault(p => p.Id == item.Product.Id);
                        if (dbProduct == null)
                        {
                            ErrorMessage = $"Sản phẩm {item.Product.Name} không tồn tại.";
                            await transaction.RollbackAsync();
                            return Page();
                        }
                        if (dbProduct.Stock < item.Quantity)
                        {
                            ErrorMessage = $"Xin lỗi, sản phẩm '{item.Product.Name}' chỉ còn {dbProduct.Stock} cái.";
                            await transaction.RollbackAsync();
                            return Page();
                        }
                    }

                    // 2. Tao don hang moi (de luu vao CSDL)
                    var newOrder = new Order
                    {
                        UserId = userId,
                        OrderDate = DateTime.Now,
                        TotalAmount = this.Total,
                        PaymentMethod = this.PaymentMethod,
                        ShippingAddress = $"{Name}, {Telephone}, {Address}",
                        Status = "Đang chờ vận chuyển"
                    };

                    // 3. TRU HANG TON KHO va them vao Don Hang
                    foreach (var item in CartItems)
                    {
                        var dbProduct = productsInDb.First(p => p.Id == item.Product.Id);
                        dbProduct.Stock -= item.Quantity; // Tru kho
                        _context.Products.Update(dbProduct);

                        var orderItem = new OrderItem
                        {
                            ProductId = item.Product.Id,
                            Quantity = item.Quantity,
                            PricePaid = item.Product.Price
                        };
                        newOrder.OrderItems.Add(orderItem);
                    }

                    // 4. Luu vao CSDL (cho Lich Su Mua Hang)
                    _context.Orders.Add(newOrder);
                    await _context.SaveChangesAsync();

                    // 5. Hoan tat giao dich
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ErrorMessage = "Đã xảy ra lỗi khi đặt hàng. " + ex.Message;
                    return Page();
                }
            }
            // --- KET THUC GIAO DICH ---

            // --- THEM LAI 2 DONG NAY ---
            // 6. Luu vao Session (cho trang MyOrder)
            var lastOrderJson = JsonSerializer.Serialize(CartItems);
            HttpContext.Session.SetString("LastOrder", lastOrderJson);
            // --- KET THUC THEM ---

            // 7. Don dep gio hang hien tai
            HttpContext.Session.Remove("Cart");

            // 8. Chuyen huong
            return RedirectToPage("/OrderSuccess");
        }

        // (Ham LoadCart() giu nguyen)
        private async Task LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson)) { return; }

            var cartIds = JsonSerializer.Deserialize<List<int>>(cartJson) ?? new List<int>();
            if (!cartIds.Any()) { return; }

            var groupedIds = cartIds
                .GroupBy(id => id)
                .Select(g => new { Id = g.Key, Count = g.Count() });

            var productIdsToFetch = groupedIds.Select(g => g.Id).ToList();
            var productsInDb = await _context.Products
                                             .Where(p => productIdsToFetch.Contains(p.Id))
                                             .ToListAsync();

            Subtotal = 0;
            TotalSavings = 0;
            CartItems.Clear();

            foreach (var item in groupedIds)
            {
                var product = productsInDb.FirstOrDefault(p => p.Id == item.Id);
                if (product != null)
                {
                    var cartItem = new CartItemViewModel
                    {
                        Product = product,
                        Quantity = item.Count
                    };
                    CartItems.Add(cartItem);
                    Subtotal += cartItem.TotalPrice;

                    if (cartItem.OriginalTotalPrice.HasValue)
                    {
                        TotalSavings += (cartItem.OriginalTotalPrice.Value - cartItem.TotalPrice);
                    }
                }
            }
            Total = Subtotal + Shipping;
        }
    }
}