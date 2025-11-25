using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ShoeShop.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context) => _context = context;

        [BindProperty]
        public Product Product { get; set; } = new Product();

        [TempData] public string SuccessMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return RedirectToPage("/Admin/Index");
            }

            Product = product;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var productInDb = await _context.Products.FindAsync(Product.Id);
            if (productInDb == null) return NotFound();

            // CHỈ CẬP NHẬT THÔNG TIN CƠ BẢN
            productInDb.Name = Product.Name;
            productInDb.Description = Product.Description;
            productInDb.Price = Product.Price;
            productInDb.OriginalPrice = Product.OriginalPrice;
            productInDb.ImageUrl = Product.ImageUrl;
            productInDb.Category = Product.Category;

            await _context.SaveChangesAsync();

            SuccessMessage = "Đã cập nhật thông tin sản phẩm!";

            // Load lại chính trang này để xem kết quả
            return RedirectToPage(new { id = Product.Id });
        }
    }
}