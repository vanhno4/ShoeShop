using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using ShoeShop.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public Product Product { get; set; } = new Product();

        // --- THEM MOI: De luu tru so luong M, L, XL ---
        [BindProperty]
        public int StockM { get; set; }
        [BindProperty]
        public int StockL { get; set; }
        [BindProperty]
        public int StockXL { get; set; }
        // --- KET THUC THEM ---

        [TempData]
        public string SuccessMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Khi tai trang, lay san pham VA cac bien the (Variants)
            var product = await _context.Products
                                        .Include(p => p.Variants) // <-- Quan trong
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return RedirectToPage("/Admin/Index");
            }

            Product = product;

            // --- THEM MOI: Dien so luong vao cac o input ---
            StockM = product.Variants.FirstOrDefault(v => v.Size == "M")?.Stock ?? 0;
            StockL = product.Variants.FirstOrDefault(v => v.Size == "L")?.Stock ?? 0;
            StockXL = product.Variants.FirstOrDefault(v => v.Size == "XL")?.Stock ?? 0;
            // --- KET THUC THEM ---

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Lay san pham va cac bien the tu CSDL de cap nhat
            var productInDb = await _context.Products
                                            .Include(p => p.Variants)
                                            .FirstOrDefaultAsync(p => p.Id == Product.Id);

            if (productInDb == null)
            {
                return NotFound();
            }

            // Cap nhat Base Information
            productInDb.Name = Product.Name;
            productInDb.Description = Product.Description;
            productInDb.Price = Product.Price;
            productInDb.OriginalPrice = Product.OriginalPrice;

            // --- THEM MOI: Cap nhat so luong cho tung size ---
            var variantM = productInDb.Variants.FirstOrDefault(v => v.Size == "M");
            if (variantM != null) variantM.Stock = StockM;

            var variantL = productInDb.Variants.FirstOrDefault(v => v.Size == "L");
            if (variantL != null) variantL.Stock = StockL;

            var variantXL = productInDb.Variants.FirstOrDefault(v => v.Size == "XL");
            if (variantXL != null) variantXL.Stock = StockXL;

            // Cap nhat TONG ton kho (de trang web cu van chay)
            productInDb.Stock = StockM + StockL + StockXL;
            // --- KET THUC THEM ---

            try
            {
                await _context.SaveChangesAsync();
                SuccessMessage = "Cập nhật sản phẩm thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage(new { id = Product.Id });
        }
    }
}