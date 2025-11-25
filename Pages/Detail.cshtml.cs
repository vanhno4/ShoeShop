using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using ShoeShop.Pages; // <-- Đảm bảo có dòng này để dùng CartSessionItem

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
        public List<Product> RelatedProducts { get; set; } = new List<Product>();

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng chọn size")]
        public string SelectedSize { get; set; } = string.Empty;

        [BindProperty]
        [Range(1, 10, ErrorMessage = "Số lượng phải từ 1 đến 10")]
        public int SelectedQuantity { get; set; } = 1;

        [TempData]
        public string? SuccessMessage { get; set; }
        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products
                                    .Include(p => p.Variants)
                                    .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                ProductFound = false;
                return Page();
            }

            Product = product;
            ProductFound = true;

            var currentCategory = Product.Category;
            RelatedProducts = await _context.Products
                .Where(p => p.Category == currentCategory && p.Id != id)
                .Take(4)
                .ToListAsync();

            return Page();
        }

        // Hàm thêm vào giỏ hàng (Không cần đăng nhập)
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return await OnGetAsync(id);
            }

            var variant = await _context.ProductVariants
                .FirstOrDefaultAsync(v => v.ProductId == id && v.Size == SelectedSize);

            if (variant == null || variant.Stock < SelectedQuantity)
            {
                ErrorMessage = $"Xin lỗi, Size {SelectedSize} chỉ còn {variant?.Stock ?? 0} sản phẩm.";
                return await OnGetAsync(id);
            }

            var cartJson = HttpContext.Session.GetString("Cart");
            List<CartSessionItem> cart;

            if (string.IsNullOrEmpty(cartJson))
            {
                cart = new List<CartSessionItem>();
            }
            else
            {
                try
                {
                    // --- ĐOẠN CODE QUAN TRỌNG ĐỂ SỬA LỖI ---
                    // Cố gắng đọc giỏ hàng
                    cart = JsonSerializer.Deserialize<List<CartSessionItem>>(cartJson) ?? new List<CartSessionItem>();
                }
                catch
                {
                    // NẾU LỖI (Do dữ liệu cũ): Reset giỏ hàng về rỗng
                    cart = new List<CartSessionItem>();
                    // Xóa dữ liệu lỗi đi
                    HttpContext.Session.Remove("Cart");
                }
            }

            var existingItem = cart.FirstOrDefault(item => item.ProductId == id && item.Size == SelectedSize);

            if (existingItem != null)
            {
                existingItem.Quantity += SelectedQuantity;
            }
            else
            {
                cart.Add(new CartSessionItem
                {
                    ProductId = id,
                    Size = SelectedSize,
                    Quantity = SelectedQuantity
                });
            }

            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));

            SuccessMessage = "Đã thêm sản phẩm vào giỏ hàng!";

            // Chuyển hướng đến trang Checkout (Hệ thống sẽ tự bắt đăng nhập nếu cần)
            return RedirectToPage("/Checkout");
        }
    }
}