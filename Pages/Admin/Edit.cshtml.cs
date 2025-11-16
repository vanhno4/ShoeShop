using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using ShoeShop.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Pages.Admin
{
    [Authorize(Roles = "Admin")] // Chỉ Admin mới vào được
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- SỬA LỖI Ở ĐÂY ---
        // Thêm (SupportsGet = true) để cho phép .NET
        // liên kết (bind) dữ liệu này khi trang được tải (GET request)
        [BindProperty(SupportsGet = true)]
        public Product Product { get; set; } = new Product();
        // --- KẾT THÚC SỬA ---

        [TempData]
        public string SuccessMessage { get; set; } = string.Empty;

        // Hàm OnGet sẽ chạy khi bạn nhấp "Sửa"
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return RedirectToPage("/Admin/Index");
            }

            Product = product; // Gán sản phẩm tìm được cho BindProperty
            return Page();
        }

        // Hàm OnPost sẽ chạy khi bạn nhấn "Save changes"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Nếu lỗi, hiển thị lại trang
            }

            // Đánh dấu sản phẩm là "Đã sửa"
            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                // Lưu thay đổi vào CSDL
                await _context.SaveChangesAsync();
                SuccessMessage = "Cập nhật sản phẩm thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                // (Xử lý lỗi nếu có)
                throw;
            }

            // Quay về trang Sửa (để xem kết quả)
            return RedirectToPage(new { id = Product.Id });
        }
    }
}