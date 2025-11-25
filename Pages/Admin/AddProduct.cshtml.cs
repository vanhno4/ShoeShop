using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeShop.Data;
using Microsoft.AspNetCore.Authorization;

namespace ShoeShop.Pages.Admin
{
    public class AddProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        [BindProperty] public int StockM { get; set; } = 50;
        [BindProperty] public int StockL { get; set; } = 50;
        [BindProperty] public int StockXL { get; set; } = 50;

        // --- THÊM DÒNG NÀY: Để gửi thông báo sang trang khác ---
        [TempData]
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. Tính tổng tồn kho
            Product.Stock = StockM + StockL + StockXL;

            // 2. Tạo sẵn các size
            Product.Variants = new List<ProductVariant>
            {
                new ProductVariant { Size = "M", Stock = StockM },
                new ProductVariant { Size = "L", Stock = StockL },
                new ProductVariant { Size = "XL", Stock = StockXL }
            };

            // 3. Lưu vào Database
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            // 4. Đặt thông báo thành công
            SuccessMessage = $"Đã thêm sản phẩm '{Product.Name}' thành công!";

            // 5. QUAN TRỌNG: Chuyển hướng về Dashboard (Trang Index)
            return RedirectToPage("/Admin/Index");
        }
    }
}