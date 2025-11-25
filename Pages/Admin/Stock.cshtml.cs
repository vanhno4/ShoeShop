using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ShoeShop.Pages.Admin
{
    public class StockModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public StockModel(ApplicationDbContext context) => _context = context;

        public Product Product { get; set; } = new Product();

        [BindProperty] public int StockM { get; set; }
        [BindProperty] public int StockL { get; set; }
        [BindProperty] public int StockXL { get; set; }

        [TempData] public string SuccessMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Variants).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return RedirectToPage("/Admin/Products");

            Product = product;
            StockM = product.Variants.FirstOrDefault(v => v.Size == "M")?.Stock ?? 0;
            StockL = product.Variants.FirstOrDefault(v => v.Size == "L")?.Stock ?? 0;
            StockXL = product.Variants.FirstOrDefault(v => v.Size == "XL")?.Stock ?? 0;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Variants).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            // Cập nhật hoặc Tạo mới Size
            UpdateVariant(product, "M", StockM);
            UpdateVariant(product, "L", StockL);
            UpdateVariant(product, "XL", StockXL);

            // Tính lại tổng kho
            product.Stock = StockM + StockL + StockXL;

            await _context.SaveChangesAsync();
            SuccessMessage = "Đã cập nhật số lượng tồn kho!";
            return RedirectToPage(new { id = id });
        }

        private void UpdateVariant(Product product, string size, int stock)
        {
            var variant = product.Variants.FirstOrDefault(v => v.Size == size);
            if (variant != null)
            {
                variant.Stock = stock;
            }
            else
            {
                product.Variants.Add(new ProductVariant { Size = size, Stock = stock });
            }
        }
    }
}