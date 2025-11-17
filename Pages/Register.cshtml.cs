using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ShoeShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ShoeShop.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có đuôi @gmail.com")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải có ít nhất 3 ký tự")]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không khớp")]
        [Display(Name = "Nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet() { }

        // Logic chung de dang ky
        private async Task<IActionResult> RegisterUser(string role)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // (Kiem tra Email/Username trung lap...)
            var existingEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if (existingEmail != null)
            {
                ErrorMessage = "Email này đã được sử dụng.";
                return Page();
            }
            var existingUsername = await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);
            if (existingUsername != null)
            {
                ErrorMessage = "Tên đăng nhập này đã được sử dụng.";
                return Page();
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

            // --- SỬA LOGIC Ở ĐÂY ---
            var newUser = new User
            {
                Email = Email,
                Username = Username,
                PasswordHash = hashedPassword,
                Role = role,
                CreatedAt = DateTime.Now // <-- Thêm ngày tham gia
            };
            // --- KẾT THÚC SỬA ---

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Login");
        }

        // Logic cho nut "Đăng Ký" (User)
        public async Task<IActionResult> OnPostUserAsync()
        {
            return await RegisterUser("User");
        }

        // Logic cho nut "Đăng ký Admin"
        public async Task<IActionResult> OnPostAdminAsync()
        {
            return await RegisterUser("Admin");
        }
    }
}