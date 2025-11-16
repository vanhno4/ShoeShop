using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ShoeShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ShoeShop.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ForgotPasswordModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { get; set; } = string.Empty;

        // --- THEM 2 TRUONG MOI ---
        [BindProperty]
        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string NewPassword { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu nhập lại không khớp")]
        [Display(Name = "Nhập lại mật khẩu mới")]
        public string ConfirmPassword { get; set; } = string.Empty;
        // --- KET THUC THEM ---

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet() { }

        // --- SUA LAI LOGIC ONPOST ---
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Neu loi (vi du: mat khau khong khop), hien thi lai trang
                return Page();
            }

            // 1. Tim user bang Username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);

            if (user == null)
            {
                // Neu khong tim thay user
                ErrorMessage = "Tên đăng nhập không tồn tại.";
                return Page();
            }

            // 2. Tim thay user! Bam mat khau moi
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(NewPassword);

            // 3. Luu vao CSDL
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // 4. Dat thong bao thanh cong va quay ve trang Login
            TempData["SuccessMessage"] = "Đổi mật khẩu thành công! Bạn có thể đăng nhập.";
            return RedirectToPage("/Login");
        }
    }
}