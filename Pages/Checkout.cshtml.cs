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
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CheckoutModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Dùng class CartViewModel đã tạo ở các bước trước
        public List<CartViewModel> CartItems { get; set; } = new List<CartViewModel>();
        public decimal Subtotal { get; set; } = 0;
        public decimal Shipping { get; set; } = 30000;
        public decimal Total { get; set; } = 0;
        public decimal TotalSavings { get; set; } = 0;

        [TempData]
        public string? ErrorMessage { get; set; }
        [TempData]
        public string? SuccessMessage { get; set; }

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
                // Nếu giỏ hàng trống, quay về trang chủ
                return RedirectToPage("/Index");
            }
            return Page();
        }

        // HAM THANH TOAN
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // 1. BẮT LỖI NHẬP LIỆU (Validation)
                // Nếu bạn quên điền tên, địa chỉ... code sẽ báo ngay dòng này
                if (!ModelState.IsValid)
                {
                    var errors = string.Join("; ", ModelState.Values
                                                .SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage));
                    ErrorMessage = "Lỗi nhập liệu: " + errors;
                    await LoadCart();
                    return Page();
                }

                await LoadCart();
                if (!CartItems.Any())
                {
                    ErrorMessage = "Giỏ hàng đang trống, vui lòng chọn sản phẩm.";
                    return RedirectToPage("/Index");
                }

                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(userIdString, out var userId))
                {
                    return RedirectToPage("/Login");
                }

                // --- BẮT ĐẦU GIAO DỊCH ---
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Kiểm tra tồn kho
                        var productVariantsInDb = new List<ProductVariant>();
                        foreach (var item in CartItems)
                        {
                            var variant = await _context.ProductVariants
                                .FirstOrDefaultAsync(v => v.ProductId == item.Product.Id && v.Size == item.Size);

                            if (variant == null)
                            {
                                throw new Exception($"Sản phẩm {item.Product.Name} (Size {item.Size}) không tìm thấy trong Database.");
                            }
                            if (variant.Stock < item.Quantity)
                            {
                                throw new Exception($"Sản phẩm '{item.Product.Name}' (Size {item.Size}) chỉ còn {variant.Stock} cái.");
                            }
                            productVariantsInDb.Add(variant);
                        }

                        // Tạo đơn hàng
                        var newOrder = new Order
                        {
                            UserId = userId,
                            OrderDate = DateTime.Now,
                            TotalAmount = this.Total,
                            PaymentMethod = this.PaymentMethod,
                            ShippingAddress = $"{Name}, {Telephone}, {Address}",
                            Status = "Đang chờ vận chuyển"
                        };

                        // Trừ kho & Thêm chi tiết đơn
                        foreach (var item in CartItems)
                        {
                            var dbVariant = productVariantsInDb.First(v => v.ProductId == item.Product.Id && v.Size == item.Size);

                            // Trừ kho Variant
                            dbVariant.Stock -= item.Quantity;
                            _context.ProductVariants.Update(dbVariant);

                            // Trừ kho Tổng (nếu có)
                            var dbProduct = await _context.Products.FindAsync(item.Product.Id);
                            if (dbProduct != null)
                            {
                                dbProduct.Stock -= item.Quantity;
                                _context.Products.Update(dbProduct);
                            }

                            // Lưu OrderItem
                            var orderItem = new OrderItem
                            {
                                ProductId = item.Product.Id,
                                Quantity = item.Quantity,
                                PricePaid = item.Product.Price,
                                Size = item.Size // <-- Đảm bảo cột này đã có trong DB
                            };
                            newOrder.OrderItems.Add(orderItem);
                        }

                        _context.Orders.Add(newOrder);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        // Ném lỗi ra ngoài để catch tổng bắt được
                        throw new Exception("Lỗi khi lưu Database: " + ex.Message + (ex.InnerException != null ? " | " + ex.InnerException.Message : ""));
                    }
                }
                // --- KẾT THÚC GIAO DỊCH ---

                // Xử lý Session sau khi thành công
                var simpleCart = CartItems.Select(ci => new {
                    ProductName = ci.Product.Name,
                    ProductImage = ci.Product.ImageUrl,
                    ProductCategory = ci.Product.Category,
                    Quantity = ci.Quantity,
                    Size = ci.Size
                }).ToList();

                HttpContext.Session.SetString("LastOrder", JsonSerializer.Serialize(simpleCart));
                HttpContext.Session.Remove("Cart");

                return RedirectToPage("/OrderSuccess");
            }
            catch (Exception ex)
            {
                // ĐÂY LÀ CHỖ QUAN TRỌNG NHẤT
                // Nó sẽ hiện chi tiết lỗi ra màn hình cho bạn đọc
                ErrorMessage = "LỖI HỆ THỐNG: " + ex.Message;
                await LoadCart();
                return Page();
            }
        }

        // --- HAM MOI: XOA SAN PHAM KHOI GIO HANG ---
        public IActionResult OnPostRemoveItem(int productId, string size)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                return RedirectToPage();
            }

            var cart = JsonSerializer.Deserialize<List<CartSessionItem>>(cartJson) ?? new List<CartSessionItem>();

            // Tim san pham de xoa (khop ca ID va Size)
            var itemToRemove = cart.FirstOrDefault(item => item.ProductId == productId && item.Size == size);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }

            // Luu lai gio hang
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            SuccessMessage = "Đã xóa sản phẩm khỏi giỏ hàng.";

            return RedirectToPage();
        }

        // --- HAM TAI GIO HANG (QUAN TRỌNG: ĐÃ SỬA ĐỂ ĐỌC ĐÚNG FORMAT MỚI) ---
        // --- THAY THẾ TOÀN BỘ HÀM LoadCart() CŨ BẰNG HÀM NÀY ---
        private async Task LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson)) { return; }

            List<CartSessionItem> cartSessionItems;

            try
            {
                // 1. Cố gắng đọc giỏ hàng
                cartSessionItems = JsonSerializer.Deserialize<List<CartSessionItem>>(cartJson) ?? new List<CartSessionItem>();
            }
            catch
            {
                // 2. NẾU LỖI (Do dữ liệu cũ): Xóa sạch giỏ hàng để tránh sập web
                HttpContext.Session.Remove("Cart");
                CartItems.Clear();
                return;
            }

            if (!cartSessionItems.Any()) { return; }

            // Lay ID san pham
            var productIdsToFetch = cartSessionItems.Select(c => c.ProductId).Distinct().ToList();

            var productsInDb = await _context.Products
                                             .Where(p => productIdsToFetch.Contains(p.Id))
                                             .ToListAsync();

            Subtotal = 0;
            TotalSavings = 0;
            CartItems.Clear();

            foreach (var item in cartSessionItems)
            {
                var product = productsInDb.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    var cartItem = new CartViewModel
                    {
                        Product = product,
                        Quantity = item.Quantity,
                        Size = item.Size
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